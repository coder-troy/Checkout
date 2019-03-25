namespace Checkout.Basket.Application
{
    using Domain;

    public class RemoveAllBasketItems
    {
        public RemoveAllBasketItems(SessionId sessionId)
        {
            SessionId = sessionId;
        }

        public SessionId SessionId { get; }
    }
}