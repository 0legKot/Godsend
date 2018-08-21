// <copyright file="SupplierController.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    /// <summary>
    /// Supplier controller
    /// </summary>
    /// <seealso cref="Controllers.EntityController{Supplier}" />
    [Route("api/[controller]")]
    public class SupplierController : EntityController<Supplier>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public SupplierController(ISupplierRepository repo, IHubContext<NotificationHub> hubContext)
            :base(hubContext)
        {
            repository = repo;
        }

        /// <summary>
        /// Details the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("[action]/{id:Guid}")]
        public Supplier Detail(Guid id)
        {
            var sup = repository.GetEntityByInfoId(id);
            repository.Watch(sup);
            return sup;
        }
    }
}
