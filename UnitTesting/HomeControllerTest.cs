using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using VisualStudio.Controllers;
using VisualStudio.Models;
using VisualStudio_UnitTesting.Models;
using Xunit;
using Moq;
namespace UnitTesting
{
    public class HomeControllerTest
    {
        class ModelCompleteFakeRepository : IRepository
        {
            public IEnumerable<Product> Products { get; } = new Product[] {
            new Product { Name = "P1", Price = 275M },
            new Product { Name = "P2", Price = 48.95M },
            new Product { Name = "P3", Price = 19.50M },
            new Product { Name = "P3", Price = 34.95M }};
            public void AddProduct(Product p)
            {
                // do nothing - not required for test
            }
        }
        [Fact]
        public void IndexActionModelIsComplete()
        {
            // Arrange
            var controller = new HomeController();
            controller.Repository = new ModelCompleteFakeRepository();
            // Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model
            as IEnumerable<Product>;
            // Assert
            Assert.Equal(controller.Repository.Products, model,
            Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
            && p1.Price == p2.Price));
        }
        class ModelCompleteFakeRepositoryPricesUnder50 : IRepository
        {
            public IEnumerable<Product> Products { get; } = new Product[] {
            new Product { Name = "P1", Price = 5M },
            new Product { Name = "P2", Price = 48.95M },
            new Product { Name = "P3", Price = 19.50M },
            new Product { Name = "P3", Price = 34.95M }};
            public void AddProduct(Product p)
            {
                // do nothing - not required for test
            }
        }
        [Fact]
        public void IndexActionModelIsCompletePricesUnder50()
        {
            // Arrange
            //Traditional
            var controller = new HomeController();
            controller.Repository = new ModelCompleteFakeRepositoryPricesUnder50();
            // Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model
            as IEnumerable<Product>;
            // Assert
            Assert.Equal(controller.Repository.Products, model,
            Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
            && p1.Price == p2.Price));
        }
       public class ModelCompleteFakeRepositoryWithParameter : IRepository
       {
            public IEnumerable<Product> Products { get; set; }
            public void AddProduct(Product p)
            {
                // do nothing - not required for test
            }
            [Theory]
            [InlineData(275, 48.95, 19.50, 24.95)]
            [InlineData(5, 48.95, 19.50, 24.95)]
            public void IndexActionModelIsCompleteWithParameter(decimal price1, decimal price2, decimal price3, decimal price4)
            {
                // Arrange
                var controller = new HomeController();
                controller.Repository = new ModelCompleteFakeRepositoryWithParameter
                {
                    Products = new Product[] {
                         new Product {Name = "P1", Price = price1 },
                         new Product {Name = "P2", Price = price2 },
                         new Product {Name = "P3", Price = price3 },
                         new Product {Name = "P4", Price = price4 },
                    }
                };
                // Act
                var model = (controller.Index() as ViewResult)?.ViewData.Model
                as IEnumerable<Product>;
                // Assert
                Assert.Equal(controller.Repository.Products, model,
                Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
                && p1.Price == p2.Price));
            }
            [Theory]
            [ClassData(typeof(ProductTestData))]
            public void IndexActionModelIsCompleteWithArrayData(Product[] products)
            {
                // Arrange 
                //traditional
                //var controller = new HomeController();
                //controller.Repository = new ModelCompleteFakeRepositoryWithParameter
                //{
                //    Products = products
                //};

                // Arrange using Mock
                var mock = new Mock<IRepository>();
                mock.SetupGet(m => m.Products).Returns(products);
                var controller = new HomeController { Repository = mock.Object };
                // Act
                var model = (controller.Index() as ViewResult)?.ViewData.Model
                as IEnumerable<Product>;
                // Assert
                Assert.Equal(controller.Repository.Products, model,
                Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
                && p1.Price == p2.Price));
            }
            class PropertyOnceFakeRepository : IRepository
            {
                public int PropertyCounter { get; set; } = 0;
                public IEnumerable<Product> Products
                {
                    get
                    {
                        PropertyCounter++;
                        return new[] { new Product { Name = "P1", Price = 100 } };
                    }
                }
                public void AddProduct(Product p)
                {
                    // do nothing - not required for test
                }
            }
            [Fact]
            public void RepositoryPropertyCalledOnce()
            {
                // Arrange traditional
                //var repo = new PropertyOnceFakeRepository();
                //var controller = new HomeController { Repository = repo };

                // Arrange using mock
                var mock = new Mock<IRepository>();
                mock.SetupGet(m => m.Products)
                .Returns(new[] { new Product { Name = "P1", Price = 100 } });
                var controller = new HomeController { Repository = mock.Object };
                // Act
                var result = controller.Index();
                // Assert traditional
                // Assert.Equal(1, repo.PropertyCounter);

                // Assert using mock
                mock.VerifyGet(m => m.Products, Times.Once);
            }
       }
    }
}
