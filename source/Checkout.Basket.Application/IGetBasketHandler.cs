namespace Checkout.Basket.Application
{
    using System.Threading.Tasks;
    using Domain;

    public interface IGetBasketHandler
    {
        Task<Basket> Handle(GetBasket message);
    }
}