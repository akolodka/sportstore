using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using Xunit;

namespace SportsStore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Can_Use_Repository()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(m => m.Products)
                .Returns((new Product[]
                {
                    new Product{ProductId = 1, Name = "P1"},
                    new Product{ProductId = 2, Name = "P2"}
                }
                ).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);

            // Act
            ProductsListViewModel result = controller.Index()?.ViewData.Model 
                as ProductsListViewModel ?? new();
            
            // Assert
            Product[] products = result.Products.ToArray();
            Assert.True(products.Length == 2);
            Assert.Equal("P1", products[0].Name);
            Assert.Equal("P2", products[1].Name);
        }

        [Fact]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(m => m.Products)
                .Returns((new Product[]
                {
                    new Product{ProductId = 1, Name = "P1"},
                    new Product{ProductId = 2, Name = "P2"},
                    new Product{ProductId = 3, Name = "P3"},
                    new Product{ProductId = 4, Name = "P4"},
                    new Product{ProductId = 5, Name = "P5"},
                }
                ).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);

            controller.PageSize = 3;

            // Act 
            ProductsListViewModel result = controller.Index(2)?.ViewData.Model
                as ProductsListViewModel ?? new(); 
                            
            // Assert
            Product[] products = result.Products.ToArray();
            Assert.True(products.Length == 2);
            Assert.Equal("P4", products[0].Name);
            Assert.Equal("P5", products[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            var mock = new Mock<IStoreRepository>();

            mock.Setup(m => m.Products)
                .Returns((new Product[]
                {
                    new Product{ProductId = 1, Name = "P1"},
                    new Product{ProductId = 2, Name = "P2"},
                    new Product{ProductId = 3, Name = "P3"},
                    new Product{ProductId = 4, Name = "P4"},
                    new Product{ProductId = 5, Name = "P5"},
                }
                ).AsQueryable<Product>());

            // Arrange
            var controller = new HomeController(mock.Object) 
            { 
                PageSize = 3
            };

            // Act
            ProductsListViewModel result = controller.Index(2)?
                                                     .ViewData
                                                     .Model as ProductsListViewModel ?? new();

            // Assert 
            PagingInfo info = result.PagingInfo;
            Assert.Equal(2, info.CurrentPage);
            Assert.Equal(3, info.ItemsPerPage);
            Assert.Equal(5, info.TotalItems);
            Assert.Equal(2, info.TotalPages);
        }
    }
}