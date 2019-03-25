namespace Checkout.Basket.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain;

    public class GetResponse
    {
        public class Item
        {
            public Item(string productId, int quantity)
            {
                ProductId = productId;
                Quantity = quantity;
            }
            
            public string ProductId { get; }
            
            public int Quantity { get; }
        }
        
        private readonly Basket _basket;

        public GetResponse(Basket basket)
        {
            _basket = basket;
        }

        public IEnumerable<Item> Items => _basket.Items.Select(i => new Item(i.ProductId.Value, i.Quantity)).ToList();
    }
}