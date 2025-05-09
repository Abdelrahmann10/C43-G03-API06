﻿using AutoMapper;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared;
using Shared.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AthenticationService(UserManager<User> userManager, IConfiguration configuration, IOptions<Jwtoptions> options, IMapper mapper)
        : IAthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            // Check If There Is User Under This Email 
            var User = await userManager.FindByEmailAsync(loginDto.Email);
            if (User == null) throw new UnAuthorizedExceptions("Incorrect Email");//Email Validation
                                                                                  //Check If The Password Is Correct For This Email
            var Result = await userManager.CheckPasswordAsync(User, loginDto.Password);
            if (!Result) throw new UnAuthorizedExceptions("Incorrect Password"); //For Password Validation
                                                                                 //Create Token Return Response

            return new UserResultDto(User.DisplayName, User.Email, await CreateTokenAsync(User));
        }
        public async Task<UserResultDto> RegisterAsync(UserRegisterDto registerDto)
        {
            var User = new User()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNamber,
            };
            var Result = await userManager.CreateAsync(User, registerDto.Password);
            if (!Result.Succeeded)
            {
                var error = Result.Errors.Select(e => e.Description).ToList();
                throw new RegisterValidationExceptions(error);
            }
            return new UserResultDto(User.DisplayName, User.Email, await CreateTokenAsync(User));

        }
        public async Task<string> CreateTokenAsync(User user)
        {
            var jwtoptions = options.Value;
            // Private Clamis
            var AuthClamis = new List<Claim>
            {
                new Claim (ClaimTypes.Name, user.UserName!),
                new Claim (ClaimTypes.Email, user.Email!)
            };
            //Add Roles To Clamis If Exist
            var Roles = await userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                AuthClamis.Add(new Claim(ClaimTypes.Role, role));
            }
            //For Secret Key 
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.SecretKey)); //For Key
            var SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            //Token
            var Token = new JwtSecurityToken(
                audience: jwtoptions.Audience,
                issuer: jwtoptions.Issure,
                expires: DateTime.UtcNow.AddDays(jwtoptions.DurationInDays),
                claims: AuthClamis,
                signingCredentials: SigningCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
        public async Task<UserResultDto> GetUserByEmail(string email)
        {
            var User = await userManager.FindByEmailAsync(email)
                ?? throw new UserNotFoundException(email);
            return new UserResultDto(
                User.DisplayName,
                User.Email,
                await CreateTokenAsync(User)
                );

        }

        public async Task<bool> CheckEmailExist(string email)
        {
            var User = await userManager.FindByEmailAsync(email);
            return User != null;
        }

        public async Task<AddressDto> GetUserAddress(string email)
        {
            var User = await userManager.Users.Include(u => u.Adderss)
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new UserNotFoundException(email);
            return mapper.Map<AddressDto>(User.Adderss);
        }

        public async Task<AddressDto> UpdateUserAddress(AddressDto address, string email)
        {
            var User = await userManager.Users.Include(s => s.Adderss)
                .FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new UserNotFoundException(email);
            if (User.Adderss != null)
            {
                User.Adderss.FirstName = address.FirstName;
                User.Adderss.LastName = address.LastName;
                User.Adderss.Street = address.Street;
                User.Adderss.City = address.City;
                User.Adderss.Country = address.Country;
            }
            else
            {
                var UserAddress = mapper.Map<Address>(address);
                User.Adderss = UserAddress;
            }
            await userManager.UpdateAsync(User);
            return address;
        }


    }
}
