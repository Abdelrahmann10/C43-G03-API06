using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation
{
    public class PaymentsController(IServiceManager serviceManager) : ApiController
    {
        #region CreateOrUpdatePaymentIntent
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var Result = await serviceManager.PaymentService.CreateOrUpdatePaymentAsync(basketId);
            return Ok(Result);
        }
        #endregion

        #region For WebHook
        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            await serviceManager.PaymentService.UpdateOrderPaymentStatus(json, Request.Headers["Stripe-Signature"]!);
            return new EmptyResult();
        }
        #endregion
    }
}
