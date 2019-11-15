using System;
using System.Collections.Generic;
using System.Text;
using VisualStudio.Models;
using Xunit;

namespace UnitTesting
{
    public class ProductTest
    {
        [Fact]
       public void CanChangeProductname()
       {
            // Arrange
            var p = new Product { Name = "Test", Price = 100M };
            // Act
            p.Name = "New Name";
            //Assert
            Assert.Equal("New Name", p.Name);
       }
        [Fact]
        public void CanChangeProductPrice()
        {
            // Arrange
            var p = new Product { Name = "Test", Price = 100M };
            // Act
            p.Price = 200M;
            //Assert
            Assert.Equal(200M, p.Price);
        }
    }
}
