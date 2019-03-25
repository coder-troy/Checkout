namespace Checkout.Basket.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application;
    using Microsoft.AspNetCore.Mvc;

    public static class PutDelegatingHandler
    {
        public static async Task<ActionResult> Handle(string sessionId, PutRequest request, Func<Task> next)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                return new BadRequestObjectResult(BadRequestMessages.InvalidSessionId);
            }

            if (request is null)
            {
                return new BadRequestObjectResult(BadRequestMessages.InvalidBody);
            }
            
            if (string.IsNullOrWhiteSpace(request.ProductId))
            {
                return new BadRequestObjectResult(BadRequestMessages.InvalidProductId);                
            }

            if (request.Quantity < 1)
            {
                return new BadRequestObjectResult(BadRequestMessages.InvalidQuantity);
            }
            
            try
            {
                await next()
                    .ConfigureAwait(false);
            }
            catch (ProductNotFoundException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }

            return new OkResult();
        }
    }
}