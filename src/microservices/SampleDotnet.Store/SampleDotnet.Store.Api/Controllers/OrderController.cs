using Microsoft.AspNetCore.Mvc;
using SampleDotnet.AspNet;
using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.DDD.Abstractions;
using SampleDotnet.Store.AppService.Checkouts.Orders;
using SampleDotnet.Store.AppService.Checkouts.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDotnet.Store.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly INotification _notification;
        private readonly IOrderAppService _orderAppService;

        public OrderController(
            INotification notification,
            IOrderAppService orderAppService)
        {
            this._notification = notification;
            this._orderAppService = orderAppService;
        }

        [HttpPost]
        public async Task<ActionResult<SubmitOrderResponseDTO>> PostSubmitOrder([FromBody] SubmitOrderCommand submitOrderCommand)
        {
            var result = await _orderAppService.SubmitOrder(submitOrderCommand);

            if (_notification.HasErrors)
                return _notification.HandleErrors();

            return Ok(result);
        }
    }
}
