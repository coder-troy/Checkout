namespace Checkout.Basket.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public static class DeleteSingleDelegatingHandler
    {
        public static async Task<ActionResult> Handle(string sessionId, string productId, Func<Task> next)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                return new BadRequestObjectResult(BadRequestMessages.InvalidSessionId);
            }

            if (string.IsNullOrWhiteSpace(productId))
            {
                return new BadRequestObjectResult(BadRequestMessages.InvalidProductId);
            }

            try
            {
                await next().ConfigureAwait(false);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }

            return new OkResult();
        }
    }
}