using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared.Security;

namespace Services
{
    public class AuthenticationService(UserManager<User> userManager,IConfiguration configuration,IOptions<JwtOptions> options,IMapper mapper) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
           var User = await userManager.FindByEmailAsync(loginDto.Email);
            if (User == null) throw new UnAuthorizedExcptions("InCorrect Email");
            var Result = await userManager.CheckPasswordAsync(User, loginDto.Password);
            if(!Result) throw new UnAuthorizedExcptions("InCorrect Password");
            return new UserResultDto
                (
                User.DisplayName,
                User.Email,
                await CreateTokenAsync(User)
                );
        }

        public async Task<UserResultDto> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var User = new User
            {
                Email = userRegisterDto.Email,
                DisplayName=userRegisterDto.DisplayName,
                PhoneNumber=userRegisterDto.PhoneNumber,
                UserName = userRegisterDto.UserName,
            };
            var Result = await userManager.CreateAsync(User,userRegisterDto.Password);
            if (!Result.Succeeded)
            {
                var errors = Result.Errors.Select(e=>e.Description).ToList();
                throw new RegisterValidationExcption(errors);
            }
            return new UserResultDto
                (
                User.DisplayName,
                User.Email,
                await CreateTokenAsync(User)
                );
        }
        public async Task<string> CreateTokenAsync(User user)
        {
            var jwtoptions = options.Value;
            var AuthClaims = new List<Claim>
            { new Claim (ClaimTypes.Name,user.UserName!),
            new Claim (ClaimTypes.Email,user.Email!)};
            var Roles=await userManager.GetRolesAsync(user);
            foreach(var role in Roles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.SecretKey));
            var SigningCredintials=new SigningCredentials(Key,SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                audience: jwtoptions.Audience,
                issuer: jwtoptions.Issure,
                expires: DateTime.UtcNow.AddDays(jwtoptions.DurationsInDays),
                claims: AuthClaims,
                signingCredentials: SigningCredintials
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
        public async Task<bool> CheckEmailExsit(string email)
        {
            var User = await userManager.FindByEmailAsync(email);
            return User != null;
        }
        public async Task<AddressDto> GetUserAddress(string email)
        {
            var User = await userManager.Users.Include(y=>y.Address)
                .FirstOrDefaultAsync(y=>y.Email== email)
                ?? throw new UserNotFoundExcption(email);
            return mapper.Map<AddressDto>(User.Address); 
        }

        public async Task<UserResultDto> GetUserByEmail(string email)
        {
            var User =  await userManager.FindByEmailAsync (email)
                ?? throw new UserNotFoundExcption (email);
            return new UserResultDto(
                User.DisplayName,
                User.Email,
                await CreateTokenAsync(User));
        }

    

        public async Task<AddressDto> UpsateUserAddress(AddressDto address, string email)
        {
            var User = await userManager.Users.Include(y => y.Address)
               .FirstOrDefaultAsync(y => y.Email == email)
               ?? throw new UserNotFoundExcption(email);
            if(User.Address!= null)
            {
                User.Address.FirstName = address.FirstName;
                User.Address.LastName = address.LastName;
                User.Address.Street = address.Street;
                User.Address.City = address.City;
                User.Address.Country = address.Country;
            }
            else
            {
                var UserAddress = mapper.Map<Address>(address);
                User.Address = UserAddress;
            }
            await userManager.UpdateAsync(User);
            return address;
        }
    }
}  
