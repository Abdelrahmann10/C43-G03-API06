using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
	public class DeliveryMethod : BaseEntity<int>
	{
        #region Ctor
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string _ShortName, string _Description, string _DeliveryTime, decimal _Price)
        {
            ShortName = _ShortName;
            Description = _Description;
            DeliveryTime = _DeliveryTime;
            Price = _Price;
        }
        #endregion
        public string ShortName { get; set; }
		public string Description { get; set; }
		public string DeliveryTime { get; set; }
		public decimal Price { get; set; }

	}
}
