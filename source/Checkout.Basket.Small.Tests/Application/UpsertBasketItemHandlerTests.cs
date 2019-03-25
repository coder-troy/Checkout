namespace Checkout.Basket.Api.Small.Tests.Application
{
    using System.Threading.Tasks;
    using Basket.Application;
    using Domain;
    using Moq;
    using Xunit;

    public class UpsertBasketItemHandlerTests
    {
        private readonly UpsertBasketItem _message;
        private readonly Mock<IBasketRepository> _basketRepository;
        private readonly Mock<IProductRepository> _productRepository;
        
        private readonly UpsertBasketItemHandler _sut;

        public UpsertBasketItemHandlerTests()
        {
            _message = new UpsertBasketItem(
                new SessionId("SESSION!"),
                new ProductId("ABC123"),
                10);
            
            _basketRepository = new Mock<IBasketRepository>();
            _productRepository = new Mock<IProductRepository>();

            _sut = new UpsertBasketItemHandler(
                _basketRepository.Object,
                _productRepository.Object);
        }

        [Fact]
        public async Task Handle_Throws_ProductNotFoundException()
        {
            // Arrange
            _productRepository
                .Setup(m => m.Load(It.Is<Product>(p => p.Id == _message.ProductId)))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => _sut.Handle(_message));
        }

        [Fact]
        public async Task Handle_Loads_the_Basket()
        {
            // Arrange
            _productRepository
                .Setup(m => m.Load(It.Is<Product>(p => p.Id == _message.ProductId)))
                .ReturnsAsync(true);

            // Act
            await _sut.Handle(_message);
            
            // Assert
            _basketRepository
                .Verify(m => m.Load(It.Is<Basket>(b => b.Id == _message.SessionId)), Times.Once);
        }
        
        [Fact]
        public async Task Handle_Saves_the_Basket()
        {
            // Arrange
            Basket basket = null;
            
            _productRepository
                .Setup(m => m.Load(It.Is<Product>(p => p.Id == _message.ProductId)))
                .ReturnsAsync(true);
            
            _basketRepository
                .Setup(m => m.Load(It.Is<Basket>(b => b.Id == _message.SessionId)))
                .Callback((Basket b) => basket = b)
                .ReturnsAsync(true);

            // Act
            await _sut.Handle(_message);
            
            // Assert
            Assert.NotNull(basket);
            var item = Assert.Single(basket.Items);
            Assert.Equal(_message.ProductId, item.ProductId);
            Assert.Equal(_message.Quantity, item.Quantity);
            
            _basketRepository.Verify(m => m.Save(basket), Times.Once);
        }
    }
}