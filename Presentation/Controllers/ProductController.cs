using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class ProductController(IServiceManager serviceManager) :ApiController
    {
        [HttpGet]
        
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationParams specs)
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync(specs);
            return Ok(products);
        }

        [HttpGet]
       
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await serviceManager.ProductService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet]
       
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }
        [HttpGet]
       
        public async Task<IActionResult> GetAllTypes()
        {
            var types = await serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }

    }
}
