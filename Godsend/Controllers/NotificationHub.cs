// <copyright file="NotificationController.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    public class NotificationHub : Hub
    {
        [Authorize]
        public async Task Send(string message)
        {
            var user = Context.User;

            var name = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            await Clients.Others.SendAsync("Receive", $"{name}: {message}");
            await Clients.Caller.SendAsync("Send", $"{name}: {message}");
        }
    }
}
