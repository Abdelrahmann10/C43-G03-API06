using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation
{
	public class AthenticationController(IServiceManager serviceManager) : ApiController
	{
		#region Login
		[HttpPost("Login")]
		public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
		{
			var Result = await serviceManager.AthenticationService.LoginAsync(loginDto);
			return Ok(Result);
		}
		#endregion
		#region Register
		[HttpPost("Register")]
		public async Task<ActionResult<UserResultDto>> Register(UserRegisterDto registerDto)
		{
			var Result = await serviceManager.AthenticationService.RegisterAsync(registerDto);
			return Ok(Result);
		}
		#endregion
	}
}
