using Godsend;
using Godsend.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using API.Tests.Common;

namespace API.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private DataContext context;
        private EFProductRepository repo;
        const string randomName = "Random name #$%^&*";

        public ProductRepositoryTests()
        {
            this.context = TestHelper.GetInMemoryContext();
            this.repo = new EFProductRepository(context, new TestSeedHelper());
        }

        [Fact]
        public void EntitiesTest()
        {
            var result = this.repo.Entities(5);

            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void SaveCreateTest()
        {
            var count = this.context.Products.Count(p => p.Info.Name == randomName);

            this.repo.SaveEntity(new Product { Info = new ProductInformation { Name = randomName } });

            Assert.Equal(count + 1, this.context.Products.Count(p => p.Info.Name == randomName));
        }

        [Fact]
        public void SaveEditTest()
        {
            var product = context.Products.First();
            var oldName = product.Info.Name;
            var newProduct = new Product { Id = product.Id, Info = new ProductInformation { Id = product.Info.Id, Name = randomName } };
            var countOldName = this.context.Products.Count(p => p.Info.Name == oldName);
            var countNewName = this.context.Products.Count(p => p.Info.Name == randomName);

            this.repo.SaveEntity(newProduct);

            Assert.Equal(countOldName - 1, this.context.Products.Count(p => p.Info.Name == oldName));
            Assert.Equal(countNewName + 1, this.context.Products.Count(p => p.Info.Name == randomName));
        }
    }
}
