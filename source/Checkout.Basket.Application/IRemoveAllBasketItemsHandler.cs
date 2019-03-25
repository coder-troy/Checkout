namespace Checkout.Basket.Application
{
    using System.Threading.Tasks;

    public interface IRemoveAllBasketItemsHandler
    {
        Task Handle(RemoveAllBasketItems message);
    }
}