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

        [HttpGet("[action]")]
        public IEnumerable<Category> GetBaseCategories()
        {
            List<Category> rootCat = new List<Category>();
            IEnumerable<Category> allCategories = (repository as IProductRepository).Categories();
            foreach (var cat in allCategories)
            {
                var cur = cat;
                while (cur.BaseCategory != null)
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

        [HttpGet("[action]/{id:Guid}")]
        public IEnumerable<Category> GetSubCategories(Guid id)
        {
            return (repository as IProductRepository).Categories().Where(x => x.BaseCategory?.Id == id).ToList();
        }

        // Low perfomance maybe
        [HttpGet("[action]")]
        public IEnumerable<Category> GetAllCategories(Guid id)
        {
            var res = new List<Category>();
            foreach (var cat in GetBaseCategories())
            {
                GetRecursiveCats(cat, ref res);
            }
            return res;
        }

        [HttpGet("[action]")]
        public IEnumerable<Information> GetByCategory(Guid id)
        {
            return repository.Entities.Where(x => x.Category.Id == id).Select(x => x.Info);
        }

        private void GetRecursiveCats(Category cur, ref List<Category> res)
        {
            res.Add(cur);
            IEnumerable<Category> subCats = GetSubCategories(cur.Id);
            if (subCats.Any())
            {
                foreach (Category curCat in subCats)
                {
                    GetRecursiveCats(curCat, ref res);
                }
            }
        }
    }
}
