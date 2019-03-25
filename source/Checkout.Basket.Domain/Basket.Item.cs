namespace Checkout.Basket.Domain
{
    public partial class Basket
    {
        public class Item
        {
            public Item(ProductId productId)
            {
                ProductId = productId;
            }
            
            public Item(ProductId productId, int quantity)
            {
                ProductId = productId;
                Quantity = quantity;
            }

            public ProductId ProductId { get; }

            public int Quantity { get; internal set; }
        }
    }
}