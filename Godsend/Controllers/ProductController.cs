// <copyright file="ProductController.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Product controller
    /// </summary>
    /// <seealso cref="Godsend.Controllers.EntityController{Godsend.Models.Product}" />
    [Route("api/[controller]")]
    public class ProductController : EntityController<Product>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public ProductController(IProductRepository repo)
        {
            repository = repo;
            Categories = (repository as IProductRepository).Categories().ToList();
        }

        /// <summary>
        /// Details the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("[action]/{id:Guid}")]
        public new ProductWithSuppliers Detail(Guid id)
        {
            var prod = (repository as IProductRepository)?.GetProductWithSuppliers(id);
            if (prod != null)
            {
                repository.Watch(prod.Product as Product);
            }

            return prod;
        }

        public class CatWithSubs
        {
            public Category Cat { get; set; }

            public IEnumerable<CatWithSubs> Subs { get; set; }

            public override string ToString()
            {
                return "CatWithSubs: " + Cat.Name;
            }
        }


        [HttpGet("[action]")]
        public IEnumerable<Category> GetBaseCategories()
        {
            List<Category> rootCat = new List<Category>();
            IEnumerable<Category> allCategories = Categories;
            var mainCat = allCategories.FirstOrDefault(x=>x.BaseCategory==null);
            foreach (var cat in allCategories)
            {
                var cur = cat;
                while (cur.BaseCategory != mainCat)
                {
                    cur = cur.BaseCategory;
                }

                if (!rootCat.Contains(cur))
                {
                    rootCat.Add(cur);
                }
            }
            return rootCat;
        }

        readonly IEnumerable<Category> Categories;

        [HttpGet("[action]/{id:Guid}")]
        public IEnumerable<Category> GetSubCategories(Guid id)
        {
            return Categories.Where(x => x.BaseCategory?.Id == id);
        }

        // Low perfomance maybe
        [HttpGet("[action]")]
        public IEnumerable<CatWithSubs> GetAllCategories()
        {
            var res = new List<CatWithSubs>();
            var mainCat = Categories.FirstOrDefault(x => x.BaseCategory == null);
            GetRecursiveCats(mainCat, ref res);
            return res.FirstOrDefault()?.Subs;
        }

        [HttpGet("[action]/{id:Guid}")]
        public IEnumerable<Information> GetByCategory(Guid id)
        {
            return repository.Entities.Where(x => x.Category?.Id == id).Select(x => x.Info);
        }

        [HttpGet("[action]/{id:Guid}")]
        public IEnumerable<object> GetPropertiesByCategory(Guid id)
        {
            return (repository as IProductRepository).Properties(id);
        }

        [HttpGet("[action]/{id:Guid}")]
        public IEnumerable<object> GetIntPropertiesByProduct(Guid id)
        {
            return (repository as IProductRepository).ProductPropertiesInt(id);
        }

        [HttpGet("[action]/{id:Guid}")]
        public IEnumerable<object> GetDecimalPropertiesByProduct(Guid id)
        {
            return (repository as IProductRepository).ProductPropertiesDecimal(id);
        }

        [HttpGet("[action]/{id:Guid}")]
        public IEnumerable<object> GetStringPropertiesByProduct(Guid id)
        {
            return (repository as IProductRepository).ProductPropertiesString(id);
        }

        private void GetRecursiveCats(Category cur, ref List<CatWithSubs> res)
        {
            CatWithSubs curCatSubs = new CatWithSubs() { Cat = cur, Subs = new List<CatWithSubs>() };
            res.Add(curCatSubs);
            curCatSubs.Subs = GetSubCategories(cur.Id).Select(x => new CatWithSubs() {Cat=x, Subs= new List<CatWithSubs>() });
            if (curCatSubs.Subs?.Any()??false)
            {
                foreach (var curCat in curCatSubs.Subs)
                {
                    GetRecursiveCats(curCat.Cat, ref res);
                }
            }
        }
    }
}
