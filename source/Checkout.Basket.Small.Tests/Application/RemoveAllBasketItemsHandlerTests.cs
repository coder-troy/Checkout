namespace Checkout.Basket.Api.Small.Tests.Application
{
    using System.Threading.Tasks;
    using Basket.Application;
    using Domain;
    using Moq;
    using Xunit;

    public class RemoveAllBasketItemsHandlerTests
    {
        private readonly Mock<IBasketRepository> _basketRepository;
        
        private readonly RemoveAllBasketItemsHandler _sut;
        private readonly RemoveAllBasketItems _message;

        public RemoveAllBasketItemsHandlerTests()
        {
            _message = new RemoveAllBasketItems(
                new SessionId("ABC1234"));

            _basketRepository = new Mock<IBasketRepository>();
            
            _sut = new RemoveAllBasketItemsHandler(_basketRepository.Object);
        }

        [Fact]
        public async Task Handle_Does_Not_Save_When_Basket_Is_Not_Found()
        {
            // Arrange
            Basket basket = null;
            
            _basketRepository
                .Setup(m => m.Load(It.Is<Basket>(b => b.Id == _message.SessionId)))
                .Callback((Basket b) => basket = b)
                .ReturnsAsync(false);
            
            // Arrange
            await _sut.Handle(_message);
            
            // Assert
            _basketRepository.Verify(m => m.Save(basket), Times.Never);
        }
        
        [Fact]
        public async Task Handle_Removes_All_Items_From_Basket_And_Saves()
        {
            // Arrange
            Basket basket = null;
            
            _basketRepository
                .Setup(m => m.Load(It.Is<Basket>(b => b.Id == _message.SessionId)))
                .Callback((Basket b) => basket = b)
                .ReturnsAsync(false);
            
            basket?.LoadItems(
                new Basket.Item(new ProductId("ABC123"), 1), 
                new Basket.Item(new ProductId("DEF098"), 3));
            
            // Arrange
            await _sut.Handle(_message);
            
            // Assert
            Assert.Empty(basket.Items);
            
            _basketRepository.Verify(m => m.Save(basket), Times.Never);
        }
    }
}