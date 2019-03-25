namespace Checkout.Basket.Api.Small.Tests.Application
{
    using System.Threading.Tasks;
    using Basket.Application;
    using Domain;
    using Moq;
    using Xunit;

    public class RemoveBasketItemHandlerTests
    {
        private readonly Mock<IBasketRepository> _basketRepository;
        private readonly Mock<IProductRepository> _productRepository;
        
        private readonly RemoveBasketItemHandler _sut;
        private readonly RemoveBasketItem _message;

        public RemoveBasketItemHandlerTests()
        {
            _message = new RemoveBasketItem(
                new SessionId("ABC1234"), 
                new ProductId("POI0987"));
            
            _basketRepository = new Mock<IBasketRepository>();
            _productRepository = new Mock<IProductRepository>();
            _productRepository
                .Setup(m => m.Load(It.Is<Product>(p => p.Id == _message.ProductId)))
                .ReturnsAsync(true);

            _sut = new RemoveBasketItemHandler(
                _productRepository.Object, 
                _basketRepository.Object);
        }

        [Fact]
        public async Task Handle_Throws_ProductNotFoundException()
        {
            // Arranges
            _productRepository
                .Setup(m => m.Load(It.Is<Product>(p => p.Id == _message.ProductId)))
                .ReturnsAsync(false);
            
            // Act
            await Assert.ThrowsAsync<ProductNotFoundException>(() => _sut.Handle(_message));
        }

        [Fact]
        public async Task Handle_Does_Not_Save_When_The_Basket_Does_Not_Exist()
        {
            // Arrange
            _basketRepository
                .Setup(m => m.Load(It.Is<Basket>(b => b.Id == _message.SessionId)))
                .ReturnsAsync(false);
            
            // Act
            await _sut.Handle(_message);
            
            // Assert
            _basketRepository.Verify(m => m.Save(It.Is<Basket>(b => b.Id == _message.SessionId)), Times.Never);
        }
        
        [Fact]
        public async Task Handle_Removes_Product_And_Saves_Basket()
        {
            // Arrange
            Basket basket = null;
            
            _basketRepository
                .Setup(m => m.Load(It.Is<Basket>(b => b.Id == _message.SessionId)))
                .Callback((Basket b) => basket = b)
                .ReturnsAsync(true);
            
            basket?.LoadItems(new Basket.Item(_message.ProductId, 1));
            
            // Act
            await _sut.Handle(_message);
            
            // Assert
            Assert.NotNull(basket);
            Assert.Empty(basket.Items);
            
            _basketRepository.Verify(m => m.Save(basket), Times.Once);
        }
    }
}