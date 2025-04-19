using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
	public class OrderItem : BaseEntity<Guid>
	{
        #region Ctor
        public OrderItem()
        {
            
        }
        public OrderItem(ProductInOrderItem _Product, int _Quantity, decimal _Price)
        {
            Product = _Product;
            Quantity = _Quantity;
            Price = _Price;
        }
        #endregion
        public ProductInOrderItem Product{ get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
