using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
	public record BasketItemDto
	{
		public int Id { get; init; }
		public string ProductName { get; init; }
		public string PictureUrl { get; init; }
		[Range(1, double.MaxValue)]
		public decimal Price { get; init; }
		public string Type { get; init; }
		public string Brand { get; init; }
		public int Quantity { get; init; }
	}
}
