using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Security;

namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        public Task<UserResultDto> LoginAsync(LoginDto loginDto);
        public Task<UserResultDto> RegisterAsync(UserRegisterDto userRegisterDto);
        public Task<UserResultDto>  GetUserByEmail(string email);
        public Task<bool> CheckEmailExsit(string email);
        public Task<AddressDto> GetUserAddress(string email);
        public Task<AddressDto> UpsateUserAddress(AddressDto address, string email);
    }
}
