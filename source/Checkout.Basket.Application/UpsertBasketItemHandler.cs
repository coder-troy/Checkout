namespace Checkout.Basket.Application
{
    using System.Threading.Tasks;
    using Domain;

    public class UpsertBasketItemHandler : IUpsertBasketItemHandler
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;

        public UpsertBasketItemHandler(
            IBasketRepository basketRepository,
            IProductRepository productRepository)
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
        }
        
        public async Task Handle(UpsertBasketItem message)
        {
            var product = new Product(message.ProductId);

            if (!await _productRepository.Load(product))
            {
                throw new ProductNotFoundException(message.ProductId);
            }
            
            var basket = new Basket(message.SessionId);
            
            await _basketRepository.Load(basket);

            basket.UpsertItem(product, message.Quantity);
            
            await _basketRepository.Save(basket);
        }
    }
}