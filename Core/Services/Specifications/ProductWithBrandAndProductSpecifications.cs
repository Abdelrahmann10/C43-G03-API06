using Domain.Contracts;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
	public class ProductWithBrandAndProductSpecifications : Specifications<Product>
	{
        //Used To Retrive Product By Id 
        public ProductWithBrandAndProductSpecifications(int id)
            :base(product => product.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }
        //Used For Get All Products
        public ProductWithBrandAndProductSpecifications(ProductSpecificationsParameters parameters)
            : base(product =>
            (!parameters.brandId.HasValue || product.BrandId == parameters.brandId.Value) &&
            (!parameters.typeId.HasValue || product.TypeId == parameters.typeId.Value)&&
            (string.IsNullOrWhiteSpace(parameters.Search)|| product.Name.ToLower().Contains(parameters.Search.ToLower().Trim())))
        {
			AddInclude(product => product.ProductBrand);
			AddInclude(product => product.ProductType);
            ApplyPagination(parameters.PageIndex, parameters.PageSize);
            
			#region Sort
            if(parameters.Sort is not null)
            {
                switch(parameters.Sort)
                {
                    case ProductSpecificationSort.NameAsc:
                        SetOrderBy(product => product.Name);
                        break;

					case ProductSpecificationSort.NameDesc:
						SetOrderByDescending(product => product.Name);
						break;

					case ProductSpecificationSort.PriceAsc:
						SetOrderBy(product => product.Price);
						break;
					case ProductSpecificationSort.PriceDes:
						SetOrderByDescending(product => product.Price);
						break;
				}
            }
			#endregion

		}
	}
}
