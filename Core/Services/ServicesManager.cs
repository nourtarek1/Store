using AutoMapper;
using Domian.Contracts;
using Domian.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManager(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IBasketRepository basketRepository,
        ICacheRepsitory cacheRepsitory,
        UserManager<AppUser> userManager
        ) : IServicesManager
    {
        public IProductService ProductService { get; } = new ProductService(unitOfWork, mapper);

        public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);

        public ICacheService CacheService { get; } = new CacheService(cacheRepsitory);

        public IAuthService AuthService { get; } = new AuthService(userManager);
    }
}
