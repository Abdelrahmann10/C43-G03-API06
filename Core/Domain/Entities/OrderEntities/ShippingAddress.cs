using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
	public class ShippingAddress
	{
        #region Ctor
        public ShippingAddress()
        {
            
        }
        public ShippingAddress(string _FirstName, string _LastName, string _Street, string _City, string _Country)
        {
            FirstName = _FirstName;
            LastName = _LastName;
            Street = _Street;
            City = _City;
            Country = _Country;
        }
        #endregion
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
	}
}
