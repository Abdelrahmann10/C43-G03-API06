using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Shared;

namespace Services.Specifications
{
    public class ProductCountSpecifications:Specifications<Product>
    {
        public ProductCountSpecifications(ProductSpecificationsParameters parameters)
            : base(product =>
            (!parameters.brandId.HasValue || product.BrandId == parameters.brandId.Value) &&
            (!parameters.typeId.HasValue || product.TypeId == parameters.typeId.Value)&&
            (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.ToLower().Trim())))

        {

        }

    }
}
