namespace Checkout.Basket.Client
{
    using System;

    public interface IBasketClientConfiguration
    {
        Uri BaseUrl { get; }
    }
}