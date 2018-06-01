using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Godsend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Godsend.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userMgr,
                               SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel creds,
                string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (await DoLogin(creds))
                {
                    return Redirect(returnUrl ?? "/");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return View(creds);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Logout(string redirectUrl)
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel creds)
        {
            if (ModelState.IsValid && await DoLogin(creds))
            {
                return Ok();
            }
            return BadRequest();
        }

        private async Task<bool> DoLogin(LoginViewModel creds)
        {
            await IdentitySeedData.EnsurePopulated(userManager);
            IdentityUser user = await userManager.FindByNameAsync(creds.Name);
            if (user != null)
            {
                await signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await signInManager.PasswordSignInAsync(user, creds.Password,
                        false, false);
                return result.Succeeded;
            }
            return false;
        }
    }

    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
