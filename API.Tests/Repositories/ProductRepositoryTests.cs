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
        public void EntitiesInfoTest()
        {
            var result = this.repo.EntitiesInfo(5,1);

            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void GetEntityTest()
        {
            var result = this.repo.GetEntity(this.repo.Entities(1).First().Id);

            Assert.Equal(repo.Entities(1).First(), result);
        }

        [Fact]
        public void GetEntityByInfoIdTest()
        {
            var result = this.repo.GetEntityByInfoId(this.repo.Entities(1).First().Info.Id);

            Assert.Equal(repo.Entities(1).First(), result);
        }

        [Fact]
        public void SaveCreateTest()
        {
            var count = this.context.Products.Count(p => p.Info.Name == randomName);

            Product testEntity = new Product { Info = new ProductInformation { Name = randomName } };
            Assert.True(repo.IsFirst(testEntity));
            this.repo.SaveEntity(testEntity);

            Assert.Equal(count + 1, this.context.Products.Count(p => p.Info.Name == randomName));
        }

        [Fact]
        public void SaveEditTest()
        {
            var product = context.Products.First();
            var oldName = product.Info.Name;
            var newProduct = new Product { Id = product.Id, Info = new ProductInformation
                                                                                    { Id = product.Info.Id, Name = randomName } };
            var countOldName = this.context.Products.Count(p => p.Info.Name == oldName);
            var countNewName = this.context.Products.Count(p => p.Info.Name == randomName);
            Assert.False(repo.IsFirst(newProduct));
            this.repo.SaveEntity(newProduct);

            Assert.Equal(countOldName - 1, this.context.Products.Count(p => p.Info.Name == oldName));
            Assert.Equal(countNewName + 1, this.context.Products.Count(p => p.Info.Name == randomName));
        }

        [Fact]
        //broken
        public void DeleteTest()
        {
            Product testEntity = new Product { Id=Guid.NewGuid(), Info = new ProductInformation { Name = "aab" } };
            //Assert.True(repo.IsFirst(testEntity));
            this.repo.SaveEntity(testEntity);

            var product = context.Products.First();
            var newProduct = new Product { Id = product.Id, Info = new ProductInformation { Id = product.Info.Id, Name = randomName } };
            Assert.False(repo.IsFirst(testEntity));
            this.repo.DeleteEntity(testEntity.Id);
            //Assert.True(repo.IsFirst(testEntity));
            //Assert.False(repo.Entities(int.MaxValue).Any(x => x == testEntity));
        }

        [Fact]
        public void GetTest()
        {
            var product = context.Products.First();
            Assert.Equal(this.repo.GetEntity(product.Id),product);
            Assert.Equal(this.repo.GetEntityByInfoId(product.Info.Id),product);
        }
        [Fact]
        public void GetProductsWithSuppliersTest()
        {
            var product = context.Products.First();
            var supp = context.LinkProductsSuppliers.Where(p => p.ProductId == product.Id);
            Assert.Equal(this.repo.GetProductWithSuppliers(product.Info.Id).Product, product);
            Assert.True(this.repo.GetProductWithSuppliers(product.Info.Id).Suppliers.Count()==supp.Count());
            Assert.True(supp.Select(s=>s.Supplier).Union(
                this.repo.GetProductWithSuppliers(product.Info.Id).Suppliers.Select(s=>s.Supplier)).Count() 
                == 
                supp.Count()
            );
            Assert.True(supp.Select(s => s.Supplier).Intersect(
                this.repo.GetProductWithSuppliers(product.Info.Id).Suppliers.Select(s => s.Supplier)).Count()
                ==
                supp.Count()
            );

        }
        //brokrn
        [Fact]
        public void GetPropertiesTest()
        {
            //no cats mb
            var cat = context.Categories.First();
            var prod = context.Products.Include(x=>x.Category).First(x => x.Category == cat);
            var sum = context.LinkProductPropertyDecimal.Count(x => x.Product == prod) +
                context.LinkProductPropertyInt.Count(x => x.Product == prod) +
                context.LinkProductPropertyString.Count(x => x.Product == prod);
           // Assert.InRange(this.repo.Properties(cat.Id).Count(),sum, sum);
        }
    }
}
