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
    using System.Net.Http;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;

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
        private User currentUser;
        private FacebookAuthSettings fbAuthSettings;

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
            ISeedHelper seedHlpr,
            IOptions<FacebookAuthSettings> fbAuthSettingsAccessor)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            roleManager = roleMngr;
            this.configuration = configuration;
            this.context = context;
            seedHelper = seedHlpr;
            this.fbAuthSettings = fbAuthSettingsAccessor.Value;
            //var currentName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            //var appUser = userManager.FindByNameAsync(currentName.Value).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Check if the current user is admin
        /// </summary>
        /// <returns>true, if the user is in Administrator role</returns>
        // DEPRECATED
        //[HttpGet("[action]")]
        //[Authorize]
        //public async Task<bool> IsAdmin()
        //{
        //    return await userManager.IsInRoleAsync(GetCurrentUser(), "Administrator");
        //}

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
            var fortst = await userManager.GetRolesAsync(GetCurrentUser());
            return fortst;
        }

        public class UserAndRole {
            public string userName { get; set; }

            public string role { get; set; }
        }

        [Authorize(Roles ="Administrator")]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddToRole([FromBody]UserAndRole userAndRole)
        {
            //if (!await IsAdmin())
            //{
            //    return BadRequest();
            //}

            User user = await userManager.FindByNameAsync(userAndRole.userName);
            if (await roleManager.FindByNameAsync(userAndRole.role) == null || user == null)
            {
                return BadRequest();
            }

            await userManager.AddToRoleAsync(user, userAndRole.role);
            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("[action]")]
        public async Task<IActionResult> ExcludeFromRole(string userName, string role)
        {
            //if (!await IsAdmin())
            //{
            //    return BadRequest();
            //}

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
                var token = await GenerateJwtToken(creds.Name, appUser);
                return Ok(new { token, appUser.Id });
            }

            return BadRequest("Invalid login attempt");
        }

        // POST api/externalauth/facebook
        [HttpPost("[action]")]
        public async Task<IActionResult> FacebookLogin([FromBody]FacebookAuthViewModel model)
        {
            var client = new HttpClient();

            // 1.generate an app access token
            var appAccessTokenResponse = await client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={fbAuthSettings.AppId}&client_secret={fbAuthSettings.AppSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);

            // 2. validate the user access token
            var tmp = await client.GetAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidationResponse = await client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                return BadRequest();
            }

            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var appUser = new User
                {
                    FacebookId = userInfo.Id,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    RegistrationDate = DateTime.Now,
                };

                var result = await userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8) + "aB$4");

                if (!result.Succeeded)
                {
                    return BadRequest();
                }
            }

            // generate the jwt for the local user...
            var localUser = await userManager.FindByEmailAsync(userInfo.Email);

            if (localUser == null)
            {
                return BadRequest();
            }

            var jwt = await GenerateJwtToken(localUser);

            return Ok(new { token = jwt, id = localUser.Id, name = localUser.UserName });
        }
    

        [Authorize]
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
        [Authorize(Roles = "Administrator,Moderator", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<ClientUser>> GetUserList(int page, int rpp)
        {
            return context.Users
                    .Skip(rpp * (page - 1)).Take(rpp)
                    .Select(u => ClientUser.FromEFUserGeneralInfo(u));
        }

        private async Task<string> GenerateJwtToken(/*string name,*/ User user)
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
    }

    public class FacebookAuthViewModel
    {
        public string AccessToken { get; set; }
    }

    public class FacebookAuthSettings
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
    }
    public class FacebookAppAccessToken
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
    public class FacebookUserAccessTokenValidation
    {
        public FacebookUserAccessTokenData Data { get; set; }
    }

    public class FacebookUserAccessTokenData
    {
        [JsonProperty("app_id")]
        public long AppId { get; set; }
        public string Type { get; set; }
        public string Application { get; set; }
        [JsonProperty("expires_at")]
        public long ExpiresAt { get; set; }
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }
        [JsonProperty("user_id")]
        public long UserId { get; set; }
    }

    public class FacebookUserData
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Locale { get; set; }
        public FacebookPictureData Picture { get; set; }
    }
    public class FacebookPictureData
    {
        public FacebookPicture Data { get; set; }
    }

    public class FacebookPicture
    {
        public int Height { get; set; }
        public int Width { get; set; }
        [JsonProperty("is_silhouette")]
        public bool IsSilhouette { get; set; }
        public string Url { get; set; }
    }
}
