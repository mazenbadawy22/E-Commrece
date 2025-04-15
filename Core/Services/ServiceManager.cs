using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketServices> _basketServices;
        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper,IBasketRepository basketRepository)
        {
            _productService = new Lazy<IProductService>(() => new ProductServices(unitOfWork, mapper));
            _basketServices = new Lazy<IBasketServices>(() => new BasketServices(basketRepository, mapper));
        }
        public IProductService ProductService => _productService.Value;

        public IBasketServices basketServices => _basketServices.Value;
    }
}
