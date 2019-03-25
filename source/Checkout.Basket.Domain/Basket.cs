namespace Checkout.Basket.Domain
{
    using System.Collections.Generic;

    public partial class Basket
    {
        private readonly SessionId _id;
        private Dictionary<ProductId, Item> _items;

        public Basket(SessionId id)
        {
            _id = id;
            _items = new Dictionary<ProductId, Item>();
        }

        public void UpsertItem(Product product, int quantity)
        {
            if (!_items.TryGetValue(product.Id, out var item))
            {
                item = new Item(product.Id);
                _items.Add(product.Id, item);
            }

            item.Quantity = quantity;
        }

        public void Remove(Product product)
        {
            _items.Remove(product.Id);
        }

        public void Clear()
        {
            _items = new Dictionary<ProductId, Item>();
        }
        
        public void LoadItems(params Item[] items)
        {
            foreach (var item in items)
            {
                _items.TryAdd(item.ProductId, item);
            }
        }

        public SessionId Id => _id;

        public IEnumerable<Item> Items => _items.Values;
    }
}