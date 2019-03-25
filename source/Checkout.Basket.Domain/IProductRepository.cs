namespace Checkout.Basket.Domain
{
    using System.Threading.Tasks;

    public interface IProductRepository
    {
        Task<bool> Load(Product product);
    }
}