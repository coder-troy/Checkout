namespace Checkout.Basket.Api.Small.Tests.Application
{
    using System.Threading.Tasks;
    using Basket.Application;
    using Domain;
    using Moq;
    using Xunit;

    public class GetBasketHandlerTests
    {
        private readonly Mock<IBasketRepository> _basketRepository;
        private readonly GetBasketHandler _sut;
        private readonly GetBasket _message;

        public GetBasketHandlerTests()
        {
            _message = new GetBasket(new SessionId("ABC123"));

            _basketRepository = new Mock<IBasketRepository>();

            _sut = new GetBasketHandler(_basketRepository.Object);
        }

        [Fact]
        public async Task Handle_Returns_An_Empty_Basket()
        {
            // Act
            var basket = await _sut.Handle(_message);
            
            // Assert
            Assert.Empty(basket.Items);
            
            _basketRepository.Verify(m => m.Load(basket), Times.Once);
        }
        
        [Fact]
        public async Task Handle_Returns_A_Loaded_Basket()
        {
            // Arrange
            Basket expectedBasket = null;
            
            _basketRepository
                .Setup(m => m.Load(It.Is<Basket>(b => b.Id == _message.SessionId)))
                .Callback((Basket b) => expectedBasket = b)
                .ReturnsAsync(true);
            
            expectedBasket?.LoadItems(new Basket.Item(new ProductId("ABC")));
            
            // Act
            var basket = await _sut.Handle(_message);
            
            // Assert
            Assert.Equal(expectedBasket, basket);
        }
    }
}