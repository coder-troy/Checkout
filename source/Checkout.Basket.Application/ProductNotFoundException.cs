namespace Checkout.Basket.Application
{
    using System;
    using Domain;

    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(ProductId id) : base($"Product with code '{id}' cannot be found.")
        {
        }
    }
}