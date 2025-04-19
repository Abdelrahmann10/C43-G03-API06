using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation
{
	public class BasketController(IServiceManager ServiceManager) : ApiController
	{
		#region Update
		[HttpPost]
		public async Task<ActionResult<BasketDto>> UpdateBasket(BasketDto basketDto)
		{
			var basket = await ServiceManager.BasketService.UpdateBasketAsync(basketDto);
			return Ok(basket);
		}
		#endregion
		#region Get
		[HttpGet("{id}")]
		public async Task<ActionResult<BasketDto>> GetBaskt(string id)
		{
			var basket = await ServiceManager.BasketService.GetBasketAsync(id);
			return Ok(basket);
		}
		#endregion
		#region Delete
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteBasket(string id)
		{
			await ServiceManager.BasketService.DeleteBasketAsync(id);
			return NoContent();
		}
		#endregion

	}
}
