using Domain.Contracts;
using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderPaymentIntentIdSpecifications : Specifications<OrderEntity>
    {
        public OrderPaymentIntentIdSpecifications(string paymentIntentId)
            : base(o => o.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
