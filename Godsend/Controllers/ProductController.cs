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
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Product controller
    /// </summary>
    /// <seealso cref="Godsend.Controllers.EntityController{Godsend.Models.Product}" />
    /// <seealso cref="Controllers.EntityController{Product}" />
    [Route("api/[controller]")]
    public class ProductController : EntityController<Product>
    {
        private readonly IEnumerable<Category> Categories;

        /// <summary>For test that server works</summary>
        [HttpGet("[action]")]
        public IActionResult SayHello() => Ok("I said Hello");

        public ProductController(ProductRepository repo, IHubContext<NotificationHub> hubContext, ILogger<Product> logger, ILogger<EntityController<Product>> logger2)
            : base(hubContext, logger2)
        {
            repository = repo;
            Categories = (repository as ProductRepository).Categories().ToList();
        }

        [HttpGet("[action]/{id:Guid}")]
        public new IActionResult Detail(Guid id)
        {
            var prod = (repository as ProductRepository)?.GetEntity(id);
            if (prod != null)
            {
                repository.Watch(prod);
            }
            else
            {
                return BadRequest();
            }

            return Ok(prod);
        }

        [HttpGet("[action]")]
        public IEnumerable<Category> GetBaseCategories()
        {
            List<Category> rootCat = new List<Category>();
            IEnumerable<Category> allCategories = Categories;
            var mainCat = allCategories.FirstOrDefault(x => x.BaseCategory == null);
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
            _logger.LogInformation($"Got base categories. First is {rootCat.FirstOrDefault().Name}");
            return rootCat;
        }

 
        [HttpGet("[action]/{id:Guid}")]
        public IEnumerable<Category> GetSubCategories(Guid id)
        {
            return Categories.Where(x => x.BaseCategory?.Id == id);
        }

        // Low perfomance maybe

        /// <summary>
        /// Gets all categories without main
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IEnumerable<CatWithSubs> GetAllCategories()
        {
            var mainCatWithSubs = new CatWithSubs()
            {
                Cat = Categories.FirstOrDefault(x => x.BaseCategory == null)
            };
            GetRecursiveCats(ref mainCatWithSubs);
            _logger.LogInformation($"Got all categories. Root is {mainCatWithSubs.Cat.Name}");
            return mainCatWithSubs.Subs;
        }

        /// <summary>
        /// Gets products by category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="skip">The skip.</param>
        /// <returns><see cref="IEnumerable{ProductInformation}"/></returns>
        [HttpGet("[action]/{id:Guid}")]
        public IEnumerable<Information> GetByCategory(Guid id, int quantity = 5, int skip = 0)
        {
            return repository.GetEntities(quantity, skip).Where(x => x.Category?.Id == id).Select(x => x.Info);
        }

        ////[HttpPost("[action]")]
        ////public IEnumerable<Information> GetByFilter([FromBody]FilterInfo filter, int quantity = 10, int skip = 0,bool invertOrder=true)
        ////{
        ////    var result = (repository as IProductRepository).GetProductInformationsByFilter(filter,quantity,skip);

        ////    return invertOrder ? result.Reverse() : result;
        ////}

        [HttpPost("[action]")]
        public ProductInfosAndCount ByFilter([FromBody]ProductFilterInfo filter)
        {
            var result = (repository as ProductRepository).GetProductInformationsByProductFilter(filter);

            return result;
        }

        [HttpGet("[action]/{categoryId:Guid}")]
        public IEnumerable<object> GetPropertiesByCategory(Guid categoryId)
        {
            var tmp = (repository as ProductRepository).Properties(categoryId);
            return tmp;
        }

        /// <summary>
        /// Gets categories in recursive way.
        /// </summary>
        /// <param name="cur">The current category with subs. It will be filled with Subcategories</param>
        private void GetRecursiveCats(ref CatWithSubs cur)
        {
            var subs = new List<CatWithSubs>();
            var curSubCats = GetSubCategories(cur.Cat.Id);
            if (curSubCats.Any())
            {
                foreach (var cat in curSubCats)
                {
                    var tmp = new CatWithSubs() { Cat = cat };
                    GetRecursiveCats(ref tmp);
                    var tmpClone = new CatWithSubs() { Cat = tmp.Cat, Subs = tmp.Subs };
                    subs.Add(tmpClone);
                }
            }

            cur.Subs = subs;
        }
    }
}
