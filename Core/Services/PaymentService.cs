using AutoMapper;
using Domain.Contracts;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;
using Domain.Exceptions;
using Domain.Exceptions.product;
using Product = Domain.Entities.Product;
using Domain.Entities.OrderEntities;
using Stripe.Climate;
using Services.Specifications;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentAsync(string basketId)
        {
            //Stripe Settings
            StripeConfiguration.ApiKey = configuration.GetRequiredSection("StripSettings")["SecretKey"];

            //Get Basket
            var basket = await basketRepository.GetBasketAsync(basketId)
                ?? throw new BasketNotFoundException(basketId);

            //Update Price Inside Basket
            foreach (var item in basket.Items)
            {
                var Product = await unitOfWork.GetRepository<Product, int>()
                    .GetAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                item.Price = Product.Price;
            }

            //Check DeliveryMethod
            if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Is Selected");
            var Method = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = Method.Price;

            //Count Total Amount
            var amount = (long)(basket.Items.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice) * 100;

            //Create Or Update PaymentIntent
            if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                //Create
                var CreateOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var PaymentIntent = await new PaymentIntentService().CreateAsync(CreateOptions);
                basket.PaymentIntentId = PaymentIntent.Id;
                basket.ClientSecret = PaymentIntent.ClientSecret;

            }
            else
            {
                //Update
                var UpdateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await new PaymentIntentService().UpdateAsync(basket.PaymentIntentId, UpdateOptions);
            }

            //Update Basket In Database
            await basketRepository.UpdateBasketAsync(basket);

            //Return BasketDto
            return mapper.Map<BasketDto>(basket);
        }

        public async Task UpdateOrderPaymentStatus(string request, string header)
        {
            //WebHook Secret
            var stripeSettingsSection = configuration.GetSection("StripSettings");
            var endPointSecret = stripeSettingsSection?["EndPointSecret"];
            //All Information About Event That Was Sent By Stripe
            if (string.IsNullOrEmpty(endPointSecret))
            {
                throw new InvalidOperationException("Stripe EndPointSecret is Missing.");
            }
            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(request, header, endPointSecret);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Stripe Event Constructions Failed: " + ex.Message);
                throw;
            }
            //That Mean Payment Complited
            var paymentIntent = stripeEvent.Data?.Object as PaymentIntent;
            if (paymentIntent == null)
            {
                Console.WriteLine("PaymentIntent Is Null In Event: " + stripeEvent.Type);
                return;
            }
            switch (stripeEvent.Type)
            {
                case "payment_intent.payment_failed":
                    await UpdatePaymentFaild(paymentIntent!.Id);
                    break;
                case "payment_intent.succeeded":
                    await UpdatePaymentRecived(paymentIntent!.Id);
                    break;
                //Handel Other Event Types
                default:
                    Console.WriteLine("Unhandled Event Type {0}", stripeEvent.Type);
                    break;
            }
        }

        private async Task UpdatePaymentRecived(string paymentIntentId)
        {
            var order = await unitOfWork.GetRepository<OrderEntity, Guid>()
                .GetByIdWithSpecificationsAsync(new OrderPaymentIntentIdSpecifications(paymentIntentId))
                ?? throw new Exception();
        }

        private async Task UpdatePaymentFaild(string paymentIntentId)
        {
            var order = await unitOfWork.GetRepository<OrderEntity, Guid>()
                .GetByIdWithSpecificationsAsync(new OrderPaymentIntentIdSpecifications(paymentIntentId))
                ?? throw new Exception();
            //Change PaymentStatus
            order.PaymentStatus = OrderPaymentStatus.PaymentRecived;
            unitOfWork.GetRepository<OrderEntity, Guid>().UpdateAync(order);
            await unitOfWork.SaveChangesAsync();

        }
    }
}
