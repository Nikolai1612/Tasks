using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Tasks.Entities;
using Tasks.Models;

namespace Tasks.Controllers
{
    public class AccountController : Controller
    {
        private List<SelectListItem> roles = new List<SelectListItem>
        {
            new SelectListItem{Value = "Admin",Text = "Admin"},
            new SelectListItem{Value = "Manager",Text = "Manager"},
            new SelectListItem { Value = "User", Text = "User" },
        };
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel
            {
                ReturnUrl = returnUrl,
                Roles = roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user,model.Password);

                if (result.Succeeded)
                {
                    var claimResult = await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, model.SelectedRole));
                    //var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    if (claimResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                    model.Roles = roles;
                    return View(model);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            model.Roles = roles;
            return View(model);
        }

        //[HttpGet]
        //public async Task<IActionResult> Login(string returnUrl)
        //{
        //    var externalProviders = await signInManager.GetExternalAuthenticationSchemesAsync();

        //    return View(new LoginViewModel
        //    {
        //        ReturnUrl = returnUrl,
        //        ExternalProviders = externalProviders
        //    });
        //}

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return PartialView("_LoginModal", model);
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, false);
            if (result.Succeeded)
            {
                return Redirect("/Home/Index");
            }

            return RedirectToAction("RegisterExternal", new ExternalLoginViewModel
            {
                ReturnUrl = returnUrl,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Name)
            });
        }

        [HttpGet]
        public IActionResult RegisterExternal(ExternalLoginViewModel model)
        {
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("RegisterExternal")]
        public async Task<IActionResult> RegisterExternalConfirmed(ExternalLoginViewModel model)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var user = new ApplicationUser(model.UserName);

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                var identityResult = await userManager.AddLoginAsync(user, info);
                if (identityResult.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return Redirect("/Home/Index");
                }
            }

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
    }
}
