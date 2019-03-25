namespace Checkout.Basket.Client
{
    using System.Threading.Tasks;

    public interface IBasketClient
    {
        Task<Basket> Get(string sessionId);
        
        Task PutItem(string sessionId, string productId, int quantity);
        
        Task DeleteItem(string sessionId, string productId);
        
        Task DeleteAllItems(string sessionId);
    }
}