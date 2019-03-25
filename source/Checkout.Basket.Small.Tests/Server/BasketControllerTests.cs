namespace Checkout.Basket.Api.Small.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;
    using Basket.Application;
    using Controllers;
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using Moq;

    public class BasketControllerTests
    {
        private const string SessionId = "1234567890";
        private const string ProductId = "ABC123";

        private readonly BasketController _sut;
        private readonly Mock<IGetBasketHandler> _getBasketHandler;
        private readonly Mock<IUpsertBasketItemHandler> _upsertBasketItemHandler;
        private readonly Mock<IRemoveBasketItemHandler> _removeBasketItemHandler;
        private readonly Mock<IRemoveAllBasketItemsHandler> _removeAllBasketItemsHandler;

        public BasketControllerTests()
        {
            _getBasketHandler = new Mock<IGetBasketHandler>();
            _upsertBasketItemHandler = new Mock<IUpsertBasketItemHandler>();
            _removeBasketItemHandler = new Mock<IRemoveBasketItemHandler>();
            _removeAllBasketItemsHandler = new Mock<IRemoveAllBasketItemsHandler>();

            _sut = new BasketController(
                _getBasketHandler.Object,
                _upsertBasketItemHandler.Object,
                _removeBasketItemHandler.Object,
                _removeAllBasketItemsHandler.Object);
        }

        [Fact]
        public async Task Get_Returns_Ok()
        {
            // Arrange
            _getBasketHandler
                .Setup(m => m.Handle(It.IsAny<GetBasket>()))
                .ReturnsAsync((GetBasket msg) => new Basket(msg.SessionId));
            
            // Act
            var result = await _sut.Get(SessionId);

            // Assert
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var response = Assert.IsType<GetResponse>(okObjectResult.Value);
            Assert.Empty(response.Items);
        }

        [Fact]
        public async Task Put_Will_Return_Bad_Request()
        {
            // Arrange
            _upsertBasketItemHandler
                .Setup(m => m.Handle(It.IsAny<UpsertBasketItem>()))
                .ThrowsAsync(new ProductNotFoundException(new ProductId(ProductId)));

            // Act
            var result = await _sut.Put(SessionId, new PutRequest());

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Put_Will_Return_OK()
        {
            // Arrange
            _upsertBasketItemHandler
                .Setup(m => m.Handle(It.IsAny<UpsertBasketItem>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _sut.Put(SessionId, new PutRequest());

            // Assert
            Assert.IsAssignableFrom<OkResult>(result);
        }

        [Fact]
        public async Task DeleteSingle_Returns_Ok()
        {
            // Arrange
            _removeBasketItemHandler
                .Setup(m => m.Handle(It.IsAny<RemoveBasketItem>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _sut.DeleteSingle(SessionId, ProductId);

            // Assert
            Assert.IsAssignableFrom<OkResult>(result);
        }

        [Fact]
        public async Task DeleteSingle_Returns_StatusCode_500()
        {
            // Arrange
            _removeBasketItemHandler
                .Setup(m => m.Handle(It.IsAny<RemoveBasketItem>()))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _sut.DeleteSingle(SessionId, ProductId);

            // Assert
            var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task DeleteAll_Returns_Ok()
        {
            // Arrange
            _removeAllBasketItemsHandler
                .Setup(m => m.Handle(It.IsAny<RemoveAllBasketItems>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _sut.DeleteAll(SessionId);

            // Assert
            Assert.IsAssignableFrom<OkResult>(result);
        }
        
        [Fact]
        public async Task DeleteAll_Returns_StatusCode_500()
        {
            // Arrange
            _removeAllBasketItemsHandler
                .Setup(m => m.Handle(It.IsAny<RemoveAllBasketItems>()))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _sut.DeleteAll(SessionId);

            // Assert
            var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}