using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.ErrorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Persentation
{
	public class ProductController(IServiceManager ServiceManager) : ApiController
	{
		#region GetAllProudcts
		[HttpGet("Products")]
		public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationsParameters parameters)
		{
			var Products = await ServiceManager.ProductService.GetAllProductsAsync(parameters);
			return Ok(Products);
		}
		#endregion
		#region GetAllBrands
		[HttpGet("Brands")]
		public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
		{
			var Brands = await ServiceManager.ProductService.GetAllBrandsAsync();
			return Ok(Brands);
		}
		#endregion
		#region GetAllTypes
		[HttpGet("Types")]
		public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
		{
			var Types = await ServiceManager.ProductService.GetAllTypeAsync();
			return Ok(Types);
		}
		#endregion
		#region GetProudctsById
		


        [HttpGet("{id}")]
		public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetProductById(int id)
		{
			var Product = await ServiceManager.ProductService.GetProductByIdAsync(id);
			return Ok(Product);
		}
		#endregion
	}
}
