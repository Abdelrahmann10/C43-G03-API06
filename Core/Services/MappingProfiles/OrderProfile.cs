using AutoMapper;
using Domain.Entities.OrderEntities;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
	public class OrderProfile : Profile
	{
        public OrderProfile()
        {
			#region Address - AddressDto
			CreateMap<ShippingAddress, ShippingAddressDto>().ReverseMap();
			#endregion
			#region OrderItem - OrderItemDto
			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(o => o.ProductId, d => d.MapFrom(s => s.Product.ProductId))
				.ForMember(o => o.PictureUrl, d => d.MapFrom(s => s.Product.PictureUrl))
				.ForMember(o => o.ProductName, d => d.MapFrom(s => s.Product.ProductName));
			#endregion
			#region OrderEntity - OrderResult
			CreateMap<OrderEntity, OrderResult>()
				.ForMember(o => o.PaymentStatus, d => d.MapFrom(s => s.ToString()))
				.ForMember(o => o.DeliveryMethod, d => d.MapFrom(s => s.DeliveryMethod.ShortName))
				.ForMember(o => o.Total, d => d.MapFrom(s => s.SubTotal+s.DeliveryMethod.Price));
			#endregion
			#region DeliveryMethod - DeliveryMethodResult
			CreateMap<DeliveryMethod, DeliveryMethodResult>().ReverseMap();
			#endregion
		}
	}
}
