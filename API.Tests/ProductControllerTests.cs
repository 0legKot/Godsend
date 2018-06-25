using Godsend;
using Godsend.Controllers;
using Godsend.Models;
using Moq;
using System;
using Xunit;

namespace API.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void DetailTest()
        {
            var repo = new Mock<IProductRepository>();

            repo.Setup(x => x.GetProductWithSuppliers(It.IsAny<Guid>()))
                .Returns(new ProductWithSuppliers { Product = new Product {Name = "Test" } });

            var controller = new ProductController(repo.Object);

            var result = controller.Detail(It.IsAny<Guid>());

            Assert.True(result.Product.Name == "Test");
        }
    }
}
