using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
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
        #region EmailExist
        //Athentication/EmailExist
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return Ok(await serviceManager.AthenticationService.CheckEmailExist(email));
        }
        #endregion
        #region GetCurrentUserByEmail
        //Athentication
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await serviceManager.AthenticationService.GetUserByEmail(email);
            return Ok(Result);
        }
        #endregion
        #region GetUserAddress
        //Athentication/Address
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await serviceManager.AthenticationService.GetUserAddress(email);
            return Ok(Result);
        }
        #endregion
        #region UpdateAddressForUser
        [Authorize]
        [HttpPut("Address")] // For Updated
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await serviceManager.AthenticationService.UpdateUserAddress(address, email);
            return Ok(Result);
        }
        #endregion
    }
}
