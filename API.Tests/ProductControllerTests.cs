using Godsend;
using Godsend.Controllers;
using Godsend.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [Fact]
        public void GetSubCategoriesTest()
        {
            var repo = new Mock<IProductRepository>();
            var cat1 = new Category() { BaseCategory = null, Name = "Test1", Id = It.IsAny<Guid>() };
            var cat2 = new Category() { BaseCategory = cat1, Name = "Test2", Id = It.IsAny<Guid>() };
            var cat3 = new Category() { BaseCategory = cat2, Name = "Test3", Id = It.IsAny<Guid>() };

            repo.Setup(x => x.Categories())
                .Returns(new List<Category>() {
                    cat1,cat2,cat3
                });

            var controller = new ProductController(repo.Object);

            IEnumerable<Category> result = controller.GetSubCategories(cat1.Id);

            Assert.True(result.All(x=>x.Id==cat2.Id));
        }
    }
}
