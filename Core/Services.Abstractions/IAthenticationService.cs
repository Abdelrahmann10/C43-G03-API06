using Shared;
using Shared.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAthenticationService
    {
        public Task<UserResultDto> LoginAsync(LoginDto loginDto);
        public Task<UserResultDto> RegisterAsync(UserRegisterDto registerDto);
        //Get Current User
        public Task<UserResultDto> GetUserByEmail(string email);
        //Check Email Exist
        public Task<bool> CheckEmailExist(string email);
        //Get User Address
        public Task<AddressDto> GetUserAddress(string email);
        //Update User Address 
        public Task<AddressDto> UpdateUserAddress(AddressDto address, string email);
    }
}
