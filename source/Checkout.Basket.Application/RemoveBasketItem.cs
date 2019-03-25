namespace Checkout.Basket.Application
{
    using Domain;

    public class RemoveBasketItem
    {
        public RemoveBasketItem(SessionId sessionId, ProductId productId)
        {
            SessionId = sessionId;
            ProductId = productId;
        }
        
        public SessionId SessionId { get; }
        
        public ProductId ProductId { get; }
    }
}