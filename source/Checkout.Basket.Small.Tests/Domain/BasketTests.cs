namespace Checkout.Basket.Api.Small.Tests
{
    using Domain;
    using Xunit;

    public class BasketTests
    {
        private readonly Product _product;
        
        private readonly Basket _basket;

        public BasketTests()
        {
            var productId = new ProductId("ABC123");
            _product = new Product(productId);
            
            _basket = new Basket(new SessionId(""));
        }

        [Fact]
        public void UpsertItem_Adds_Product_To_The_Basket()
        {
            // Act
            _basket.UpsertItem(_product, 1);
            
            // Assert
            var item = Assert.Single(_basket.Items);
            Assert.Equal(_product.Id, item.ProductId);
            Assert.Equal(1, item.Quantity);
        }

        [Fact]
        public void UpsertItem_Modifies_Product_Already_In_Basket()
        {
            // Arrange
            _basket.UpsertItem(_product, 1);

            // Act
            _basket.UpsertItem(_product, 10);

            // Assert
            var item = Assert.Single(_basket.Items);
            Assert.Equal(_product.Id, item.ProductId);
            Assert.Equal(10, item.Quantity);
        }

        [Fact]
        public void Remove_Does_Nothing_When_The_Basket_Is_Empty()
        {
            // Pre-Assert
            Assert.Empty(_basket.Items);
            
            // Act
            _basket.Remove(_product);
            
            // Assert
            Assert.Empty(_basket.Items);
        }
        
        [Fact]
        public void Remove_Removes_Product_From_Basket()
        {
            // Arrange 
            _basket.UpsertItem(_product, 1);
            
            // Pre-Assert
            Assert.NotEmpty(_basket.Items);
            
            // Act
            _basket.Remove(_product);
            
            // Assert
            Assert.Empty(_basket.Items);
        }

        [Fact]
        public void Clear_Removes_All_Products_From_Basket()
        {
            // Arrange
            _basket.UpsertItem(_product, 1);

            // Act
            _basket.Clear();
            
            // Assert
            Assert.Empty(_basket.Items);
        }
        
        [Fact]
        public void Clear_Does_Nothing_When_Basket_Is_Empty()
        {
            // Act
            _basket.Clear();
            
            // Assert
            Assert.Empty(_basket.Items);
        }
    }
}