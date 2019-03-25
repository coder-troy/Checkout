namespace Checkout.Basket.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public static class DeleteAllDelegatingHandler
    {
        public static async Task<ActionResult> Handle(string sessionId, Func<Task> next)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                return new BadRequestObjectResult(BadRequestMessages.InvalidSessionId);
            }
            
            try
            {
                await next().ConfigureAwait(false);
            }
            catch (Exception )
            {
                return new StatusCodeResult(500);
            }

            return new OkResult();
        }
    }
}