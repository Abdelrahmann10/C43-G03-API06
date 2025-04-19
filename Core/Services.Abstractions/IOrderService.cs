using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
	public interface IOrderService
	{
		//Get Order By Id =>
		public Task<OrderResult> GetOrderByIdAsync(Guid id);
		//Get Order For User By Email => 
		public Task<IEnumerable<OrderResult>> GetOrderByEmailAsync(string email);
		//Create Order =>
		public Task<OrderResult> CreateOrderAsync(OrderRequest request, string useremail);
		//Get All Delivey Method =>
		public Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
	}
}
