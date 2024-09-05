using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tasks.Entities;
using Tasks.Models;

namespace Tasks.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(new { message = "User registered successfully!" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok(new { message = "User logged successfully!" });
                }
                return Unauthorized(new { message = "Invalid login attempt." });
            }
            return BadRequest(ModelState);
        }

        [HttpGet("external-sign-in")]
        public IActionResult ExternalSignIn(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action(nameof(ExternalSignInCallback), "Account", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet("external-sign-in-callback")]
        public async Task<IActionResult> ExternalSignInCallback(string returnUrl)
        {
            var info = await signInManager.GetExternalLoginInfoAsync(); 
            if (info == null)
            {
                return Redirect(returnUrl);
            }

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("SignUpExternal", new ExternalSignUpModel  
            {
                ReturnUrl = returnUrl,
                UserName = info.Principal.FindFirstValue(ClaimTypes.GivenName)
            });
        }

        [HttpGet]
        public IActionResult SignUpExternal(ExternalSignUpModel model)
        {
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("SignUpExternal")]
        public async Task<IActionResult> SignUpExternalConfirmed(ExternalSignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var info = await signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return Redirect(model.ReturnUrl);
                }

                var user = new ApplicationUser(model.UserName);

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    var identityResult = await userManager.AddLoginAsync(user, info);
                    if (identityResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
                        return Redirect(model.ReturnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet("accessdenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost("sign-out")]
        public async Task<IActionResult> UserSignOut()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
    }
}