using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
	public record OrderResult
	{
		public Guid Id { get; set; }
		//UserEmail
		public string UserEmail { get; set; }
		//Address
		public ShippingAddressDto ShippingAddress { get; set; }
		//OrderItems
		public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
		//PaymentStatus
		public string PaymentStatus { get; set; }
		//DeliveryMethod
		public string DeliveryMethod { get; set; }
		//SubTotal == items.Quantity * Price
		public decimal SubTotal { get; set; }
		//Payment 
		public string PaymentIntentId { get; set; } = string.Empty;
		//OrderDate
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
		public decimal Total { get; set; }
	}
}
