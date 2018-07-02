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
    /// <seealso cref="Controllers.EntityController{Product}" />
    [Route("api/[controller]")]
    public class ProductController : EntityController<Product>
    {
        /// <summary>
        /// The categories
        /// </summary>
        private readonly IEnumerable<Category> Categories;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController" /> class.
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
        [HttpGet("[action]")]
        public IActionResult SayHello()
        {
            return Ok("I said Hello");
        }
        /// <summary>
        /// Gets the base categories.
        /// </summary>
        /// <returns></returns>
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

            return rootCat;
        }

        /// <summary>
        /// Gets the sub categories.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
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
            return repository.Entities(quantity, skip).Where(x => x.Category?.Id == id).Select(x => x.Info);
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
            var result = (repository as IProductRepository).GetProductInformationsByProductFilter(filter);

            return result;
        }


        /// <summary>
        /// Gets the properties by category.
        /// </summary>
        /// <param name="categoryId">The identifier.</param>
        /// <returns></returns>
        [HttpGet("[action]/{categoryId:Guid}")]
        public IEnumerable<object> GetPropertiesByCategory(Guid categoryId)
        {
            var tmp = (repository as IProductRepository).Properties(categoryId);
            return tmp;
        }

        /////// <summary>
        /////// Gets the int properties by product.
        /////// </summary>
        /////// <param name="id">The identifier.</param>
        /////// <returns></returns>
        ////[HttpGet("[action]/{id:Guid}")]
        ////public IEnumerable<object> GetIntPropertiesByProduct(Guid id)
        ////{
        ////    return (repository as IProductRepository).ProductPropertiesInt(id);
        ////}

        /////// <summary>
        /////// Gets the decimal properties by product.
        /////// </summary>
        /////// <param name="id">The identifier.</param>
        /////// <returns></returns>
        ////[HttpGet("[action]/{id:Guid}")]
        ////public IEnumerable<object> GetDecimalPropertiesByProduct(Guid id)
        ////{
        ////    return (repository as IProductRepository).ProductPropertiesDecimal(id);
        ////}

        /////// <summary>
        /////// Gets the string properties by product.
        /////// </summary>
        /////// <param name="id">The identifier.</param>
        /////// <returns></returns>
        ////[HttpGet("[action]/{id:Guid}")]
        ////public IEnumerable<object> GetStringPropertiesByProduct(Guid id)
        ////{
        ////    return (repository as IProductRepository).ProductPropertiesString(id);
        ////}

        /// <summary>
        /// Gets the recursive cats.
        /// </summary>
        /// <param name="cur">The current.</param>
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
