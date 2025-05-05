using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServicesManager
    {
         IProductService ProductService { get;  }
         IBasketService BasketService { get; }
         ICacheService CacheService { get; }
         IAuthService AuthService { get; }
         IOrderSrvice orderSrvice { get; }
    }
}
