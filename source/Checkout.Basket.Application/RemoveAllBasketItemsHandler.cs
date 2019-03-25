namespace Checkout.Basket.Application
{
    using System.Threading.Tasks;
    using Domain;

    public class RemoveAllBasketItemsHandler : IRemoveAllBasketItemsHandler
    {
        private readonly IBasketRepository _basketRepository;

        public RemoveAllBasketItemsHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task Handle(RemoveAllBasketItems message)
        {
            var basket = new Basket(message.SessionId);

            if (!await _basketRepository.Load(basket))
            {
                return;
            }
            
            basket.Clear();
            
            await _basketRepository.Save(basket);
        }
    }
}