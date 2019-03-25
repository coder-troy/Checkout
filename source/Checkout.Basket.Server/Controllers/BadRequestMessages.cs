namespace Checkout.Basket.Controllers
{
    public static class BadRequestMessages
    {
        public const string InvalidSessionId = "Session Id cannot be empty";
        public const string InvalidProductId = "Product Id cannot be empty";
        public const string InvalidQuantity = "Item quantity cannot be less than 1";
        public const string InvalidBody = "Body cannot be empty.";
    }
}