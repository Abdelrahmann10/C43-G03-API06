using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Security
{
	public record UserRegisterDto
	{
		[Required(ErrorMessage ="Name Is Required !!")]
		public string DisplayName { get; init; }
		[Required(ErrorMessage = "Email Is Required !!")]
		[EmailAddress]
		public string Email { get; init; }
		[Required(ErrorMessage = "Password Is Required !!")]
		public string Password { get; init; }
		[Required(ErrorMessage = "UserName Is Required !!")]
		public string UserName { get; init; }
		public string? PhoneNamber { get; init; }

	}
}
