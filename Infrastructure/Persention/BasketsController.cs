using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persention
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
             var result =await servicesManager.BasketService.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(BasketDto basket)
        {
            var result = await servicesManager.BasketService.UpdateBasketAsync(basket);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string id)
        {
             await servicesManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }

    }



}
