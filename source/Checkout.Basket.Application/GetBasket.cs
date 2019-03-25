namespace Checkout.Basket.Application
{
    using Domain;

    public class GetBasket
    {
        public GetBasket(SessionId sessionId)
        {
            SessionId = sessionId;
        }
        
        public SessionId SessionId { get; }
    }
}