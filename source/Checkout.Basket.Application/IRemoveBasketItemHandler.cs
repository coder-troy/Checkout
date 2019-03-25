namespace Checkout.Basket.Application
{
    using System;
    using System.Threading.Tasks;

    public interface IRemoveBasketItemHandler
    {
        Task Handle(RemoveBasketItem message);
    }
}