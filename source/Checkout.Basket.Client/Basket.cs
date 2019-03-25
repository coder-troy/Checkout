namespace Checkout.Basket.Client
{
    using System.Collections.Generic;

    public class Basket
    {
        public class Item
        {
            public string ProductId { get; set; }

            public int Quantity { get; set; }
        }
        
        public List<Item> Items { get; set; }
    }
}