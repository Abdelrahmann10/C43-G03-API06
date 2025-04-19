using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Basket;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Domain.Exceptions.product;
using Microsoft.AspNetCore.Identity.UI.Services;
using Services.Abstractions;
using Services.Specifications;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class OrderService(IMapper mapper, IUnitOfWork unitOfWork, IBasketRepository basketRepository) : IOrderService
	{
		public async Task<OrderResult> CreateOrderAsync(OrderRequest request, string useremail)
		{
			//Address
			var Address = mapper.Map<ShippingAddress>(request.ShippingAddress);
			//2-OrderItem => Basket => BasketItem
			var Basket = await basketRepository.GetBasketAsync(request.BasketId)
				?? throw new BasketNotFoundException(request.BasketId);
			var OrderItems = new List<OrderItem>();
			foreach(var item in Basket.Items)
			{
				var Product = await unitOfWork.GetRepository<Product, int>()
					.GetAsync(item.Id)?? throw new ProductNotFoundException(item.Id);
				OrderItems.Add(CreateOrderItem(item, Product));
			}
			//3-Delivery
			var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
				.GetAsync(request.DeliverMethodId)
				?? throw new DeliveryMethodNotFoundException(request.DeliverMethodId);
			//4- SubTotal
			var SubTotal = OrderItems.Sum(item => item.Price * item.Quantity);
			//Save To DataBase
			var Order = new OrderEntity(useremail, Address, OrderItems, DeliveryMethod, SubTotal);
			await unitOfWork.GetRepository<OrderEntity, Guid>()
				.AddAsync(Order);
			await unitOfWork.SaveChangesAsync();
			//Map And Return
			return mapper.Map<OrderResult>(Order);
		}

		private OrderItem CreateOrderItem(BasketItem item, Product Product)
		=> new OrderItem(new ProductInOrderItem(Product.Id, Product.Name, Product.PictureUrl),
			item.Quantity, Product.Price);

		public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
		{
			var Methods = await unitOfWork.GetRepository<DeliveryMethod, int>()
				.GetAllAsync();
			return mapper.Map<IEnumerable<DeliveryMethodResult>>(Methods);
		}

		public async Task<IEnumerable<OrderResult>> GetOrderByEmailAsync(string email)
		{
			var Orders = await unitOfWork.GetRepository<OrderEntity, Guid>()
				.GetAllWithSpecificationsAsync(new OrderWithIncludeSpecifications(email));
			return mapper.Map<IEnumerable<OrderResult>>(Orders);
		}

		public async Task<OrderResult> GetOrderByIdAsync(Guid id)
		{
			var Order = await unitOfWork.GetRepository<OrderEntity, Guid>()
				.GetByIdWithSpecificationsAsync(new OrderWithIncludeSpecifications(id))
				??throw new OrderNotFoundException(id);
			return mapper.Map<OrderResult>(Order);
		}
	}
}
