namespace Checkout.Basket.Application
{
    using System.Threading.Tasks;
    using Domain;

    public class RemoveBasketItemHandler : IRemoveBasketItemHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly IBasketRepository _basketRepository;

        public RemoveBasketItemHandler(
            IProductRepository productRepository,
            IBasketRepository basketRepository)
        {
            _productRepository = productRepository;
            _basketRepository = basketRepository;
        }
        
        public async Task Handle(RemoveBasketItem message)
        {
            var product = new Product(message.ProductId);

            if (!await _productRepository.Load(product))
            {
                throw new ProductNotFoundException(message.ProductId);
            }
            
            var basket = new Basket(message.SessionId);

            if (!await _basketRepository.Load(basket))
            {
                return; 
            }
            
            basket.Remove(product);
            
            await _basketRepository.Save(basket);
        }
    }
}