namespace Checkout.Basket.Application
{
    using Domain;

    public class UpsertBasketItem
    {
        public UpsertBasketItem(
            SessionId sessionId, 
            ProductId productId, 
            int quantity)
        {
            SessionId = sessionId;
            ProductId = productId;
            Quantity = quantity;
        }
        
        public SessionId SessionId { get;  }
        
        public ProductId ProductId { get; }
        
        public int Quantity { get;  }
    }
}