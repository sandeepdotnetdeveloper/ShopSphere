using ShopSphere.Api.Models;
using ShopSphere.Api.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSphere.Tests
{
    public class ProductCatalogTests
    {
        [Fact]
        public void GetAll_ShouldReturnProducts()
        {
            // Arrange
            var catalog = new ProductCatalog();

            // Act
            var products = catalog.GetAll();

            // Assert
            Assert.NotEmpty(products);
            Assert.Equal(3, products.Count);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectProduct()
        {
            // Arrange
            var catalog = new ProductCatalog();

            // Act
            var product = catalog.GetById(1);

            // Assert
            Assert.NotNull(product);
            Assert.Equal("Mechanical Keyboard", product.Name);
        }

        [Fact]
        public void Add_ShouldCreateNewProduct()
        {
            // Arrange
            var catalog = new ProductCatalog();

            var product = new Product
            {
                Name = "Gaming Headset",
                Price = 3499,
                Stock = 12
            };

            // Act
            var result = catalog.Add(product);

            // Assertfgdfkgh
            Assert.Equal(4, result.Id);
            Assert.Equal("Gaming Headset", result.Name);
        }

        [Fact]
        public void Delete_ShouldRemoveProduct()
        {
            // Arrange
            var catalog = new ProductCatalog();

            // Act
            var result = catalog.Delete(1);

            // Assert
            Assert.True(result);
            Assert.Null(catalog.GetById(1));
        }
    }
}
