namespace Checkout.Basket.Infrastructure
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;

    public class BasketRepository : IBasketRepository
    {
        private static ConcurrentDictionary<SessionId, List<Basket.Item>> _items = new ConcurrentDictionary<SessionId, List<Basket.Item>>
        {
            [new SessionId("67098CDF-848F-4C24-9919-C70CB700B935")] = new List<Basket.Item>
            {
                new Basket.Item(ProductId.One, 10)
            },
            [new SessionId("AAE081E0-D036-4511-9BD4-5D5940AE21CE")] = new List<Basket.Item>
            {
                new Basket.Item(ProductId.One, 1),
                new Basket.Item(ProductId.Two, 1)                
            }
        };
        
        public async Task<bool> Load(Basket basket)
        {
            if (_items.TryGetValue(basket.Id, out var items))
            {
                basket.LoadItems(items.ToArray());
                return true;
            }

            return false;
        }

        public async Task Save(Basket basket)
        {
            if (_items.TryGetValue(basket.Id, out var items))
            {
                items.Clear();
                items.AddRange(basket.Items);
            }
            else
            {
                _items.TryAdd(basket.Id, basket.Items.ToList());
            }
        }
    }
}