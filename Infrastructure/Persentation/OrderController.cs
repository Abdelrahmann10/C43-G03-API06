using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persentation
{
	[Authorize]
	public class OrderController(IServiceManager serviceManager) : ApiController
	{
		#region Create
		[HttpPost]
		public async Task<ActionResult<OrderResult>> Create(OrderRequest request)
		{
			var Email = User.FindFirstValue(ClaimTypes.Email);
			var Order = await serviceManager.OrderService.CreateOrderAsync(request, Email);
			return Ok(Order);
		}
		#endregion
		#region GetOrdersByEmail
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderResult>>> GetOrders()
		{
			var Email = User.FindFirstValue(ClaimTypes.Email);
			var Order = await serviceManager.OrderService.GetOrderByEmailAsync(Email);
			return Ok(Order);
		}
		#endregion
		#region GetOrdersById
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderResult>> GetOrdersById(Guid id)
		{
			var Order = await serviceManager.OrderService.GetOrderByIdAsync(id);
			return Ok(Order);
		}
		#endregion
		#region GetDeliveryMethods
		[AllowAnonymous]
		[HttpGet("DeliveryMethods")]
		public async Task<ActionResult<DeliveryMethodResult>> GetDeliveryMethods()
		{
			var Methods = await serviceManager.OrderService.GetDeliveryMethodsAsync();
			return Ok(Methods);
		}
		#endregion

	}
}
