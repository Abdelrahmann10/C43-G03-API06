using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.product
{
	public class OrderNotFoundException(Guid id) 
		:NotFoundException($"No Order With Id {id} Was Found") 
	{
	}
}
