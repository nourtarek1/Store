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
    public class ProductsController(IServicesManager servicesManager) :ControllerBase
    {
        // endpoint : public non-static method

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductSpecificationsParamter productSpecificationsParamter)
        {

            var result = await servicesManager.ProductService.GetAllProductsAsync(productSpecificationsParamter);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await servicesManager.ProductService.GetProductByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await servicesManager.ProductService.GetAllBrandAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetAllTyps()
        {
            var result = await servicesManager.ProductService.GetAllTypesAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

    }
}
