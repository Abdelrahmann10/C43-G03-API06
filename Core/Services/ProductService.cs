using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions.product;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
	{
		public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
		{
			var Brand = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
			var BrandsResutl = mapper.Map<IEnumerable<BrandResultDto>>(Brand);
			return BrandsResutl;
		}

		public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters parameters)
		{
			var proudct = await unitOfWork.GetRepository<Product,int>().GetAllWithSpecificationsAsync(new ProductWithBrandAndProductSpecifications(parameters));
			var ProudctResult = mapper.Map<IEnumerable<ProductResultDto>>(proudct);
			var Count  = ProudctResult.Count();
			var totalCount = await unitOfWork.GetRepository<Product, int>()
				.CountAsync(new ProductCountSpecifications(parameters));

            var Result = new PaginatedResult<ProductResultDto>
				(
					parameters.PageIndex,
					parameters.PageSize,
					totalCount,
					ProudctResult

				);
			return Result;
			 
		}

		public async Task<IEnumerable<TypeResultDto>> GetAllTypeAsync()
		{
			var Type = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
			var TypesResutl = mapper.Map<IEnumerable<TypeResultDto>>(Type);
			return TypesResutl;
		}

		public async Task<ProductResultDto?> GetProductByIdAsync(int id)
		{
			var Proudct = await unitOfWork.GetRepository<Product, int>().GetByIdWithSpecificationsAsync(new ProductWithBrandAndProductSpecifications(id));
			return Proudct is null? throw new ProductNotFoundException(id) : mapper.Map<ProductResultDto>(Proudct);
			
		}
	}
}
