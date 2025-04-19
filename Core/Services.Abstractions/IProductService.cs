using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
	public interface IProductService
	{
		public Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters parameters);
		public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
		public Task<IEnumerable<TypeResultDto>> GetAllTypeAsync();
		public Task<ProductResultDto?> GetProductByIdAsync(int id);
	}
}
