using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CoreIdentity.Models.Account;
using CoreIdentity.Repository.DbModels;
using Microsoft.AspNetCore.Identity;

namespace CoreIdentity.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;

            public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            [AllowAnonymous]
            public IActionResult Login(string returnUrl)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(LoginModel model, string returnUrl)
            {
                if (ModelState.IsValid)
                {
                    AppUser user = await _userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        await _signInManager.SignOutAsync();
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

                        if (result.Succeeded)
                            return Redirect(returnUrl ?? "/"); // Success.
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(LoginModel.Email), "Invalid email or password");
                    }
                }

                ViewBag.ReturnUrl = returnUrl;
                return View(model); // Failure.
            }

            [Authorize]
            public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
    }
}