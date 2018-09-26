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
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
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
        private RoleManager<Role> roleManager;
        private IConfiguration configuration;
        private DataContext context;
        private ISeedHelper seedHelper;
        private string token;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userMgr">User manager</param>
        /// <param name="signInMgr">Sign in manager</param>
        /// <param name="configuration">Configuration</param>
        public AccountController(
            UserManager<User> userMgr,
            SignInManager<User> signInMgr,
            IConfiguration configuration,
            DataContext context,
            RoleManager<Role> roleMngr,
            ISeedHelper seedHlpr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            roleManager = roleMngr;
            this.configuration = configuration;
            this.context = context;
            seedHelper = seedHlpr;
        }

        /// <summary>
        /// Check is the current user admin
        /// </summary>
        /// <returns>true, if the user is in Administrator role</returns>
        [HttpGet("[action]")]
        public async Task<bool> IsAdmin()
        {
            return await userManager.IsInRoleAsync(GetCurrentUser(), "Administrator");
        }

        private User GetCurrentUser()
        {
            var currentName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            return context.Users.FirstOrDefault(u => u.UserName == currentName.Value);
        }

        /// <summary>
        /// Get roles of current user
        /// </summary>
        /// <returns>List of string roles</returns>
        [HttpGet("[action]")]
        public async Task<IEnumerable<string>> GetRoles()
        {
            return await userManager.GetRolesAsync(GetCurrentUser());
        }

        public class UserAndRole {
            public string userName { get; set; }

            public string role { get; set; }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddToRole([FromBody]UserAndRole userAndRole)
        {
            if (!await IsAdmin())
            {
                return BadRequest();
            }

            User user = await userManager.FindByNameAsync(userAndRole.userName);
            if (await roleManager.FindByNameAsync(userAndRole.role) == null || user == null)
            {
                return BadRequest();
            }

            await userManager.AddToRoleAsync(user, userAndRole.role);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ExcludeFromRole(string userName, string role)
        {
            if (!await IsAdmin())
            {
                return BadRequest();
            }

            User user = await userManager.FindByNameAsync(userName);
            if (await roleManager.FindByNameAsync(role) == null || user == null)
            {
                return BadRequest();
            }

            await userManager.RemoveFromRoleAsync(user, role);
            return Ok();
        }

        /// <summary>
        /// Get all roles that exist in db
        /// </summary>
        /// <returns>List all roles</returns>
        [HttpGet("[action]")]
        public IEnumerable<string> GetAllRoles()
        {
            return roleManager.Roles.Select(x => x.Name);
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
        public async Task<IActionResult> Login([FromBody] LoginViewModel creds)
        {
            seedHelper.EnsurePopulated(context);
            //IdentitySeedData.EnsurePopulated(userManager, roleManager);
            var result = await signInManager.PasswordSignInAsync(creds.Name, creds.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = await userManager.FindByNameAsync(creds.Name);
                token = await GenerateJwtToken(creds.Name, appUser);
                return Ok(new { token, appUser.Id });
            }

            return BadRequest("Invalid login attempt");
        }


        //TODO do something
        [HttpPost("[action]")]
        public async Task<object> EditProfile(/*string token,*/ [FromBody] RegisterViewModel model)
        {
            User user = context.Users.FirstOrDefault(x => x.UserName == User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value);
            user.Email = model.Email;
            user.Birth = DateTime.Parse(model.Birth);
            user.UserName = model.Name;
            user.NormalizedEmail = userManager.NormalizeKey(model.Email);
            user.NormalizedUserName = userManager.NormalizeKey(model.Name);
            if (model.Password != null)
            {
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.Password);
            }

            var res = await context.SaveChangesAsync();
            if (res == 1)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("Could not edit");
            }
        }

        [HttpPost("[action]")]
        public async Task<object> Register([FromBody] RegisterViewModel model)
        {
            var user = new User
            {
                UserName = model.Name,
                Email = model.Email,
                Birth = DateTime.Parse(model.Birth),
                RegistrationDate = DateTime.Now
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //await signInManager.SignInAsync(user, false);
                //currentUser = user;
                //var token = await GenerateJwtToken(model.Email, user);
                return await Login(new LoginViewModel() {Name = user.UserName, Password = model.Password });
            }

            return BadRequest("Could not register");
        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ClientUser GetProfile(string id)
        {
            var currentName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            var user = context.Users.FirstOrDefault(u => u.Id == id);
            var clientUser = user.UserName == currentName.Value ?
                ClientUser.FromEFUser(user) :
                ClientUser.FromEFUserGeneralInfo(user);

            return clientUser;
        }

        [HttpGet("[action]/{page:int}/{rpp:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<ClientUser>> GetUserList(int page, int rpp)
        {
            if (await IsAdmin())
            {
                return context.Users
                    .Skip(rpp * (page - 1)).Take(rpp)
                    .Select(u => ClientUser.FromEFUserGeneralInfo(u));
            }
            else
            {
                return new List<ClientUser>();
            }
        }

        private async Task<string> GenerateJwtToken(string name, User user)
        {
            var principal = await signInManager.CreateUserPrincipalAsync(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    configuration["JwtIssuer"],
                    configuration["JwtIssuer"],
                    principal.Claims,
                    expires: DateTime.UtcNow.AddDays(30),
                    signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       /* // todo review
        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, name), // subject
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // jwt id
                new Claim( ClaimTypes.NameIdentifier,user.Id)
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
        }*/
    }
}
