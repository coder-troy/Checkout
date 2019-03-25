namespace Checkout.Basket.Client
{
    using System.Threading.Tasks;
    using Flurl;
    using Flurl.Http;
    using Flurl.Http.Configuration;
    using Newtonsoft.Json;

    public class BasketClient : IBasketClient
    {
        private readonly IBasketClientConfiguration _configuration;

        public BasketClient(IBasketClientConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Basket> Get(string sessionId)
        {
            return await new Url(_configuration.BaseUrl)
                .AppendPathSegment("api")
                .AppendPathSegment("basket")
                .AppendPathSegment(sessionId)
                .GetJsonAsync<Basket>();
        }
        
        public async Task PutItem(string sessionId, string productId, int quantity)
        {
            await new Url(_configuration.BaseUrl)
                .AppendPathSegment("api")
                .AppendPathSegment("basket")
                .AppendPathSegment(sessionId)
                .PutJsonAsync(new
                {
                    ProductId= productId, 
                    Quantity = quantity
                });
        }

        public async Task DeleteItem(string sessionId, string productId)
        {
            await new Url(_configuration.BaseUrl)
                .AppendPathSegment("api")
                .AppendPathSegment("basket")
                .AppendPathSegment(sessionId)
                .AppendPathSegment("product")
                .AppendPathSegment(productId)
                .DeleteAsync();
        }

        public async Task DeleteAllItems(string sessionId)
        {
            await new Url(_configuration.BaseUrl)
                .AppendPathSegment("api")
                .AppendPathSegment("basket")
                .AppendPathSegment(sessionId)
                .DeleteAsync();
        }
    }
}