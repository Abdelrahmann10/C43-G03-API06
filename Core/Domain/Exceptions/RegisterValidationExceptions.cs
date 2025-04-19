using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
	public class RegisterValidationExceptions : Exception
	{
		public IEnumerable<string> Errors { get; set; }
        public RegisterValidationExceptions(IEnumerable<string> errors)
            : base("Register Validation Errors") 
        {
            Errors = errors;
        }
    }
}
