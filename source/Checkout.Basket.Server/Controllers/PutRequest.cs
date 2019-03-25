namespace Checkout.Basket.Controllers
{
    public class PutRequest
    {
        public string ProductId { get; set; }
        
        public int Quantity { get; set; }
    }
}