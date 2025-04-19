using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
	internal class ProductConfigurations : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			#region Product 
			builder.Property(product=>product.Price)
				.HasColumnType("decimal(18,3)");
			#endregion
			#region ProductBrand 
			builder.HasOne(product => product.ProductBrand)
				.WithMany()
				.HasForeignKey(product => product.BrandId);
			#endregion
			#region ProductType
			builder.HasOne(product => product.ProductType)
				.WithMany()
				.HasForeignKey(product => product.TypeId);
			#endregion
		}
	}
}
