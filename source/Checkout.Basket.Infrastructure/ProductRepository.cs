namespace Checkout.Basket.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    public class ProductRepository: IProductRepository
    {
        private static readonly List<Domain.ProductId> _productIds = new List<Domain.ProductId>
        {
            ProductId.One,
            ProductId.Two
        };
        
        public Task<bool> Load(Product product)
        {
            return Task.FromResult(_productIds.Contains(product.Id));
        }
    }
}