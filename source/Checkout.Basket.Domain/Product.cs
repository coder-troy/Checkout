namespace Checkout.Basket.Domain
{
    public class Product
    {
        public Product(ProductId id)
        {
            Id = id;
        }
        
        public ProductId Id { get; }
    }
}