// <copyright file="NotificationController.cs" company="Godsend Team">
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


    [Route("api/[controller]")]
    public class NotificationController : EntityController<Supplier>
    {

        public NotificationController()
        {
        }

        [HttpGet("[action]")]
        public void Notify()
        {

        }
    }
}
