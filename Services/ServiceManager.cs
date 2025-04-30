using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketServices> _basketServices;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper,IBasketRepository basketRepository,UserManager<User>userManager,IOptions<JwtOptions> options)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketServices = new Lazy<IBasketServices>(() => new BasketService(basketRepository, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, mapper,options));
        }
        public IProductService ProductService => _productService.Value;
        public IBasketServices BasketService => _basketServices.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        IProductService IServiceManager.ProductService => throw new NotImplementedException();

        IBasketServices IServiceManager.BasketServices => throw new NotImplementedException();
    }
    

    
}
