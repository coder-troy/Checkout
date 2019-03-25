namespace Checkout.Basket.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application;
    using Domain;
    using Microsoft.AspNetCore.Mvc;

    public class BasketController : Controller
    {
        private readonly IGetBasketHandler _getBasketHandler;
        private readonly IUpsertBasketItemHandler _upsertBasketItemHandler;
        private readonly IRemoveBasketItemHandler _removeBasketItemHandler;
        private readonly IRemoveAllBasketItemsHandler _removeAllBasketItemsHandler;

        public BasketController(
            IGetBasketHandler getBasketHandler,
            IUpsertBasketItemHandler upsertBasketItemHandler,
            IRemoveBasketItemHandler removeBasketItemHandler, 
            IRemoveAllBasketItemsHandler removeAllBasketItemsHandler)
        {
            _getBasketHandler = getBasketHandler;
            _upsertBasketItemHandler = upsertBasketItemHandler;
            _removeBasketItemHandler = removeBasketItemHandler;
            _removeAllBasketItemsHandler = removeAllBasketItemsHandler;
        }

        [HttpGet]
        [Route("api/basket/{sessionId}")]
        public async Task<ActionResult> Get([FromRoute] string sessionId)
        {
            var basket = await _getBasketHandler
                .Handle(
                    new GetBasket(
                        new SessionId(sessionId)))
                .ConfigureAwait(false);
            
            return Ok(new GetResponse(basket));
        }

        [HttpPut]
        [Route("api/basket/{sessionId}")]
        public async Task<ActionResult> Put([FromRoute] string sessionId, [FromBody] PutRequest request)
        {
            try
            {
                await _upsertBasketItemHandler
                    .Handle(
                        new UpsertBasketItem(
                            new SessionId(sessionId),
                            new ProductId(request.ProductId),
                            request.Quantity))
                    .ConfigureAwait(false);
            }
            catch (ProductNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                // Log out message
                return StatusCode(500);
            }
            
            return Ok();
        }

        [HttpDelete]
        [Route("api/basket/{sessionId}/product/{productId}")]
        public async Task<ActionResult> DeleteSingle([FromRoute] string sessionId, [FromRoute] string productId)
        {
            try
            {
                await _removeBasketItemHandler
                    .Handle(
                        new RemoveBasketItem(
                            new SessionId(sessionId), 
                            new ProductId(productId)))
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
            
            return Ok();
        }
        
        [HttpDelete]
        [Route("api/basket/{sessionId}")]
        public async Task<ActionResult> DeleteAll([FromRoute] string sessionId)
        {
            try
            {
                await _removeAllBasketItemsHandler
                    .Handle(
                        new RemoveAllBasketItems(
                            new SessionId(sessionId)))
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
            
            return Ok();
        }
    }
}