using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BasketController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> Get(string id)
        {
            var basket = await serviceManager.BasketServices.GetBasketAsync(id);
            return Ok(basket);


        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> Update([FromBody] BasketDto basketDto)
        {
            var basket = await serviceManager.BasketServices.UpdateBasketAsync(basketDto);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
             await serviceManager.BasketServices.DeleteBasketAsync(id);
            
            return NoContent();
        }
    }
}
