namespace Checkout.Basket.Domain
{
    using System.Threading.Tasks;

    public interface IBasketRepository
    {
        Task<bool> Load(Basket basket);
        
        Task Save(Basket basket);
    }
}