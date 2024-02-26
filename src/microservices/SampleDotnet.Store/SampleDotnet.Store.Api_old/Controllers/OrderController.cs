using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using SampleDotnet.AspNet;
using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.DDD.Abstractions;
using SampleDotnet.Store.AppService.Checkouts.Orders;
using SampleDotnet.Store.AppService.Checkouts.Orders.DTOs;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDotnet.Store.Api.Controllers
{
    /// <summary>
    /// Controller to manage customer orders
    /// </summary>
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

        /// <summary>
        /// Submit a new order for the client
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///      {
        ///         "createdAt": "2021-08-29T22:14:15.944Z",
        ///         "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "items": [
        ///           {
        ///             "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "quantity": 1,
        ///             "value": 100
        ///           }
        ///         ],
        ///         "payment": {
        ///           "paymentMethod": "Credit",
        ///           "cardName": "a name",
        ///           "cardNumber": "5555555555554444",
        ///           "expiration": "2021-08-29T22:14:15.944Z",
        ///           "securityCode": 123,
        ///           "value": 100
        ///         }
        ///      }
        /// 
        /// </remarks>
        /// <param name="submitOrderCommand">Order and payment data</param>
        /// <returns>Order id and internal track number</returns>
        [HttpPost]
        //[Authorize(Policy = "AdministrationPolicy")] // author's remarks: keeping without authentication and authorization for now
        [SwaggerResponse(200, null, typeof(SubmitOrderResponseDTO))]
        [SwaggerResponse(400, null, typeof(ErrorDto))]
        [SwaggerResponse(500, null, typeof(ErrorDto))]
        public async Task<ActionResult<SubmitOrderResponseDTO>> PostSubmitOrder([FromBody] SubmitOrderCommand submitOrderCommand)
        {
            var result = await _orderAppService.SubmitOrder(submitOrderCommand);

            if (_notification.HasErrors)
                return _notification.HandleErrors();

            return Ok(result);
        }
    }
}
