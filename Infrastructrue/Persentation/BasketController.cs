using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Persentation
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BasketController(IServiceManager ServiceManager):ControllerBase
    {
        #region Update
        [HttpPost]
        public async Task<ActionResult<BasketDto>> UpdateBasket(BasketDto basketDto)
        {
            var basket = await ServiceManager.basketServices.UpdateBasketAsync(basketDto);
            return Ok(basket);
        }

        #endregion
        #region Get
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> GetBasket (string id)
        {
            var Basket = await ServiceManager.basketServices.GetBasketAsync(id);
            return Ok(Basket);
        }
        #endregion
        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            await ServiceManager.basketServices.DeleteBasketAsync(id);
            return NoContent();
        }
        #endregion

    }
}
