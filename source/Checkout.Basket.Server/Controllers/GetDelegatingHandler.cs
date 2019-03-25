namespace Checkout.Basket.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public static class GetDelegatingHandler 
    {
        public static async Task<ActionResult> Handle(string sessionId, Func<Task<GetResponse>> next)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                return new BadRequestObjectResult(BadRequestMessages.InvalidSessionId);
            }
            
            return new OkObjectResult(await next().ConfigureAwait(false));
        }
    }
}