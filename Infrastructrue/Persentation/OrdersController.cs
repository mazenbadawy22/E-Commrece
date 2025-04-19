using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Order;

namespace Persentation
{
    [Authorize]
    public class OrdersController(IServiceManager serviceManager):ApiController
    {
        #region Create
        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrder (OrderRequest request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Order = await serviceManager.orderServices.CreateOrderAsync(request,email);
            return Ok(Order);
        }
        #endregion
        #region GetByEmail
        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await serviceManager.orderServices.GetOrdersByEmailAsync(email);
            return Ok(Orders);
        }
        #endregion
        #region GetByID
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResultDto>> GetOrders (Guid id)
        {
            var Order = await serviceManager.orderServices.GetOrderByIdAsync(id);
            return Ok(Order);
        }

        #endregion
        #region DeliveryMethod
        [AllowAnonymous]
        [HttpGet("deliverymethod")]
        public async Task<ActionResult<DeliveryMethodDto>> GetAllDeliveryMethod()
        {
            var Delivery = await serviceManager.orderServices.GetDeliveryMethodsAsync();
            return Ok(Delivery);
        } 
        #endregion
    }
}
