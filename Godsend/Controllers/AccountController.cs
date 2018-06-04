﻿// <copyright file="AccountController.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Account controller
    /// </summary>
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userMgr">User manager</param>
        /// <param name="signInMgr">Sign in manager</param>
        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        /// <summary>
        /// Do login
        /// </summary>
        /// <param name="returnUrl">Return url</param>
        /// <returns>View</returns>
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Do login
        /// </summary>
        /// <param name="creds">Credentials</param>
        /// <param name="returnUrl">Return url</param>
        /// <returns>View or redirect</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel creds, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (await DoLogin(creds))
                {
                    return Redirect(returnUrl ?? "/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }
            }

            return View(creds);
        }

        /// <summary>
        /// Do logout
        /// </summary>
        /// <param name="redirectUrl">Redirect url</param>
        /// <returns>Ok</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Logout(string redirectUrl)
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// Do login
        /// </summary>
        /// <param name="creds">Credentials</param>
        /// <returns>True or false</returns>
        [HttpPost("[action]")]
        public async Task<bool> Login([FromBody] LoginViewModel creds)
        {
            if (ModelState.IsValid && await DoLogin(creds))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Do login
        /// </summary>
        /// <param name="creds">Credentials</param>
        /// <returns>True or false</returns>
        private async Task<bool> DoLogin(LoginViewModel creds)
        {
            await IdentitySeedData.EnsurePopulated(userManager);
            IdentityUser user = await userManager.FindByNameAsync(creds.Name);
            if (user != null)
            {
                await signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await signInManager.PasswordSignInAsync(user, creds.Password, false, false);
                return result.Succeeded;
            }

            return false;
        }
    }
}
