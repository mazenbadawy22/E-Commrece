using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared.Security;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketServices> _basketServices;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IOrderServices> _orderServices;
        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper,IBasketRepository basketRepository,UserManager<User> userManager,IOptions<JwtOptions> options,IConfiguration configuration)
        {
            _productService = new Lazy<IProductService>(() => new ProductServices(unitOfWork, mapper));
            _basketServices = new Lazy<IBasketServices>(() => new BasketServices(basketRepository, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,configuration,options));
            _orderServices=new Lazy<IOrderServices>(()=>new OrderServices(mapper,basketRepository,unitOfWork));
        }
        public IProductService ProductService => _productService.Value;

        public IBasketServices basketServices => _basketServices.Value;

        public IAuthenticationService authenticationService => _authenticationService.Value;

        public IOrderServices orderServices => _orderServices.Value;
    }
}
