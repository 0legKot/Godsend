﻿// <copyright file="ProductController.cs" company="Godsend Team">
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
    }
}
