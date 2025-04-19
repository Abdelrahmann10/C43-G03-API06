using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
	public class OrderEntity : BaseEntity<Guid>
	{
        #region Ctor
        public OrderEntity()
        {
            
        }
        public OrderEntity(string _UserEmail, ShippingAddress _ShippingAddress, ICollection<OrderItem> _OrderItems, DeliveryMethod _DeliveryMethod, decimal _SubTotal)
        {
            UserEmail = _UserEmail;
			ShippingAddress = _ShippingAddress;
			OrderItems = _OrderItems;
			DeliveryMethod = _DeliveryMethod;
			SubTotal = _SubTotal;
		}
        #endregion
        //UserEmail
        public string UserEmail { get; set; }
		//Address
		public ShippingAddress ShippingAddress { get; set; }
		//OrderItems
		public ICollection<OrderItem> OrderItems { get; set; }
		//PaymentStatus
		public OrderPaymentStatus PaymentStatus { get; set; }
		//DeliveryMethod
		public int? DeliveryMethodId { get; set; }
		public DeliveryMethod DeliveryMethod { get; set; }
		public decimal SubTotal { get; set; }
		//Payment
		public string PaymentIntentId { get; set; } = string.Empty;
		//OrderDate
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

	}
}
