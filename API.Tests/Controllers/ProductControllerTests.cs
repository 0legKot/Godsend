namespace API.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Godsend;
    using Godsend.Controllers;
    using Godsend.Models;
    using Moq;
    using Xunit;

    public class ProductControllerTests
    {
        [Fact]
        public void GetSubCategoriesTest()
        {
            var repo = new Mock<IProductRepository>();
            var cat1 = new Category() { BaseCategory = null, Name = "Test1", Id = It.IsAny<Guid>() };
            var cat2 = new Category() { BaseCategory = cat1, Name = "Test2", Id = It.IsAny<Guid>() };
            var cat3 = new Category() { BaseCategory = cat2, Name = "Test3", Id = It.IsAny<Guid>() };

            repo.Setup(x => x.Categories())
                .Returns(new List<Category>()
                {
                    cat1, cat2, cat3
                });

            var controller = new ProductController(repo.Object);

            IEnumerable<Category> result = controller.GetSubCategories(cat1.Id);

            Assert.True(result.All(x => x.Id == cat2.Id));
        }

        [Fact]
        public void DetailTest()
        {
            var repo = new Mock<IProductRepository>();

            repo.Setup(x => x.GetProductWithSuppliers(It.IsAny<Guid>()))
                .Returns(new ProductWithSuppliers { Product = new Product() });

            var controller = new ProductController(repo.Object);

            var result = controller.Detail(It.IsAny<Guid>());

            Assert.True(result.Product != null);
        }

        [Fact]
        public void GetAllCategoriesTest()
        {
            var repo = new Mock<IProductRepository>();
            var cat1 = new Category() { BaseCategory = null, Name = "Test1", Id = Guid.NewGuid() };
            var cat2 = new Category() { BaseCategory = cat1, Name = "Test2", Id = Guid.NewGuid() };
            var cat3 = new Category() { BaseCategory = cat2, Name = "Test3", Id = Guid.NewGuid() };
            var cat4 = new Category() { BaseCategory = cat1, Name = "Test4", Id = Guid.NewGuid() };

            repo.Setup(x => x.Categories())
                .Returns(new List<Category>() {
                    cat1, cat2, cat3, cat4
                });

            var controller = new ProductController(repo.Object);

            IEnumerable<CatWithSubs> result = controller.GetAllCategories();

            Assert.Equal<int>(2, result.Count());
            Assert.Equal<int>(1, result.First(x => x.Cat == cat2).Subs.Count());
        }

        [Fact]
        public void GetByCategoryTest()
        {
            var repo = new Mock<IProductRepository>();
            var cat1 = new Category() { BaseCategory = null, Name = "Test1", Id = Guid.NewGuid() };
            var cat2 = new Category() { BaseCategory = cat1, Name = "Test2", Id = Guid.NewGuid() };
            var cat3 = new Category() { BaseCategory = cat2, Name = "Test3", Id = Guid.NewGuid() };
            var cat4 = new Category() { BaseCategory = cat1, Name = "Test4", Id = Guid.NewGuid() };

            repo.Setup(x => x.Categories())
                .Returns(new List<Category>() {
                    cat1, cat2, cat3, cat4
                });
            repo.Setup(x => x.Entities(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<Product>() {
                new Product() { Category = cat2 },
                new Product() { Category = cat3 },
                new Product() { Category = cat2 },
                new Product() { Category = cat1 }
            });
            var controller = new ProductController(repo.Object);

            IEnumerable<Information> result = controller.GetByCategory(cat2.Id);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetPropertiesByCategory()
        {
            var repo = new Mock<IProductRepository>();
            var cat1 = new Category() { BaseCategory = null, Name = "Test1", Id = Guid.NewGuid() };
            var cat2 = new Category() { BaseCategory = cat1, Name = "Test2", Id = Guid.NewGuid() };
            var cat3 = new Category() { BaseCategory = cat2, Name = "Test3", Id = Guid.NewGuid() };
            var cat4 = new Category() { BaseCategory = cat1, Name = "Test4", Id = Guid.NewGuid() };

            repo.Setup(x => x.Categories())
                .Returns(new List<Category>() {
                    cat1, cat2, cat3, cat4
                });
            repo.Setup(x => x.Properties(It.IsAny<Guid>())).Returns(new List<Property>() {
                new Property() {RelatedCategory = cat2 },
                new Property() {RelatedCategory = cat2 }
            });
            var controller = new ProductController(repo.Object);

            IEnumerable<object> result = controller.GetPropertiesByCategory(cat2.Id);

            Assert.Equal(2, result.Count());
        }
    }
}
