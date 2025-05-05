using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persention
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController(
        IServicesManager servicesManager
        
        ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequestDto request)
        {
            var email =  User.FindFirstValue(ClaimTypes.Email);
             var result = await servicesManager.orderSrvice.CreateOrderAsync(request, email);
            return Ok(result);

        }


        [HttpGet]
        public async Task<IActionResult> GetOrders( )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await servicesManager.orderSrvice.GetOrdersByUserEmailAsync(email);
            return Ok(result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersById(Guid id)
        {
            var result = await servicesManager.orderSrvice.GetOrderByIdAsync( id);
            return Ok(result);

        }

        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethod( )
        {
            var result = await servicesManager.orderSrvice.GetAllDeliveryMethods();
            return Ok(result);

        }
    }
}
