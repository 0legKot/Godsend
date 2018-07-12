// <copyright file="AccountController.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Account controller
    /// </summary>
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userMgr">User manager</param>
        /// <param name="signInMgr">Sign in manager</param>
        /// <param name="configuration">Configuration</param>
        public AccountController(UserManager<User> userMgr, SignInManager<User> signInMgr, IConfiguration configuration)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            this.configuration = configuration;
        }

        /////// <summary>
        /////// Do login
        /////// </summary>
        /////// <param name="returnUrl">Return url</param>
        /////// <returns>View</returns>
        ////[HttpGet]
        ////public IActionResult Login(string returnUrl)
        ////{
        ////    ViewBag.returnUrl = returnUrl;
        ////    return View();
        ////}

        /////// <summary>
        /////// Do login
        /////// </summary>
        /////// <param name="creds">Credentials</param>
        /////// <param name="returnUrl">Return url</param>
        /////// <returns>View or redirect</returns>
        ////[HttpPost]
        ////public async Task<IActionResult> Login(LoginViewModel creds, string returnUrl)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        if (await DoLogin(creds))
        ////        {
        ////            return Redirect(returnUrl ?? "/");
        ////        }
        ////        else
        ////        {
        ////            ModelState.AddModelError(string.Empty, "Invalid username or password");
        ////        }
        ////    }

        ////    return View(creds);
        ////}

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
        public async Task<IActionResult> Login([FromBody] LoginViewModel creds)
        {
            ////if (ModelState.IsValid && await DoLogin(creds))
            ////{
            ////    return true;
            ////}

            ////return false;
            await IdentitySeedData.EnsurePopulated(userManager);
            var result = await signInManager.PasswordSignInAsync(creds.Name, creds.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = await userManager.FindByNameAsync(creds.Name);
                var token = GenerateJwtToken(creds.Name, appUser);
                return Ok(new { token });
            }

            return BadRequest("Invalid login attempt");
        }

        [HttpPost("[action]")]
        public async Task<object> Register([FromBody] RegisterViewModel model)
        {
            var user = new User
            {
                UserName = model.Name,
                Email = model.Email,
                Birth = model.Birth,
                RegistrationDate = DateTime.Now
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                var token = GenerateJwtToken(model.Email, user);
                return Ok(new { token });
            }

            return BadRequest("Could not register");
        }

        /////// <summary>
        /////// Do login, not used
        /////// </summary>
        /////// <param name="creds">Credentials</param>
        /////// <returns>True or false</returns>
        ////private async Task<bool> DoLogin(LoginViewModel creds)
        ////{
        ////    await IdentitySeedData.EnsurePopulated(userManager);
        ////    User user = await userManager.FindByNameAsync(creds.Email);
        ////    if (user != null)
        ////    {
        ////        await signInManager.SignOutAsync();
        ////        Microsoft.AspNetCore.Identity.SignInResult result =
        ////            await signInManager.PasswordSignInAsync(user, creds.Password, false, false);
        ////        return result.Succeeded;
        ////    }

        ////    return false;
        ////}

        private string GenerateJwtToken(string name, User user)
        {
            // todo review
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, name), // subject
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // jwt id
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
