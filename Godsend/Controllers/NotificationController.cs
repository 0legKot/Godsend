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
    using Microsoft.AspNetCore.SignalR;

    public class NotificationController : Hub
    {
        public async Task Send(string message)
        {
            await Clients.Others.SendAsync("Send", message);
        }
    }
}
