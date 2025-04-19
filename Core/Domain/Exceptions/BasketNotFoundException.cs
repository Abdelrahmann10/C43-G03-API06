using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
	public class BasketNotFoundException : NotFoundException
	{
        public BasketNotFoundException(string id)
            : base($"The Basket With Id {id} Not Found")
        {
            
        }
    }
}
