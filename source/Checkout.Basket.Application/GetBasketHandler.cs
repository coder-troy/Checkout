namespace Checkout.Basket.Application
{
    using System.Threading.Tasks;
    using Domain;

    public class GetBasketHandler : IGetBasketHandler
    {
        private readonly IBasketRepository _basketRepository;

        public GetBasketHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        
        public async Task<Basket> Handle(GetBasket message)
        {
            var basket = new Basket(message.SessionId);

            await _basketRepository.Load(basket);

            return basket;
        }
    }
}