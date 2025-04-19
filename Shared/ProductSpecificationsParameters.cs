using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
	public class ProductSpecificationsParameters
	{
		private const int MaximumPageSize = 10;
		private const int DefaultPageSize= 5;
		public int? typeId { get; set; }
		public int? brandId { get; set; }
		public ProductSpecificationSort? Sort { get; set; }
		public int PageIndex { get; set; } =1;
		private int _pageSize;
		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = value>MaximumPageSize ?MaximumPageSize : value;
		}
        public string? Search { get; set; }

    }
    public enum ProductSpecificationSort
	{
		NameAsc,
		NameDesc,
		PriceAsc,
		PriceDes
	}
}
