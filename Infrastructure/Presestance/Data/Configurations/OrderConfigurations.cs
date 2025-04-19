using Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
	public class OrderConfigurations : IEntityTypeConfiguration<OrderEntity>
	{
		public void Configure(EntityTypeBuilder<OrderEntity> builder)
		{
			//For ShippingAddress
			builder.OwnsOne(o => o.ShippingAddress, s => s.WithOwner());
			//For OrderItem 
			builder.HasMany(o => o.OrderItems).WithOne();
			// Payment
			builder.Property(o => o.PaymentStatus)
				.HasConversion(s => s.ToString(),
				s => Enum.Parse<OrderPaymentStatus>(s));

			//For DeliveryMethod
			builder.HasOne(o => o.DeliveryMethod).WithMany()
				.OnDelete(DeleteBehavior.SetNull);

			//SubTotal
			builder.Property(o => o.SubTotal).HasColumnType("decimal(18, 3)");
		}
	}
}
