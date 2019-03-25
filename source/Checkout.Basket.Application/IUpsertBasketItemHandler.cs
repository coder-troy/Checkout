namespace Checkout.Basket.Application
{
    using System.Threading.Tasks;

    public interface IUpsertBasketItemHandler
    {
        Task Handle(UpsertBasketItem message);
    }
}