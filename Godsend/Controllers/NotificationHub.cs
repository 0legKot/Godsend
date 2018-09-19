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
        public async Task SendTo(string to, string message)
        {
            var user = Context.User;
            string name = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            await Clients.Group(to).SendAsync("Receive", $"{name}: {message}");
            await Clients.Caller.SendAsync("Send", $"{name}: {message}");
        }

        [Authorize]
        public async Task Send(string message)
        {
            var user = Context.User;

            var name = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            await Clients.Others.SendAsync("Receive", $"{name}: {message}");
            await Clients.Caller.SendAsync("Send", $"{name}: {message}");
        }

        public override Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;
            Groups.AddToGroupAsync(Context.ConnectionId, name).GetAwaiter().GetResult();

            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, name);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
