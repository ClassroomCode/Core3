using EComm.Data.Entities;
using EComm.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace EComm.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, (2 + 2));
        }

        [Fact]
        public void ProductDetails()
        {
            // Arrange
            var repository = new StubRepository();
            var pc = new ProductController(repository, null, null);

            // Act
            var result = pc.Details(1).Result;

            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
            var vr = result as ViewResult;
            Assert.IsAssignableFrom<Product>(vr.Model);
            var model = vr.Model as Product;
            Assert.Equal("Bread", model.ProductName);
        }
    }
}
