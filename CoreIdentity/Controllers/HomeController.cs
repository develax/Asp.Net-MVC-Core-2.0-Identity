using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreIdentity.Models.Home;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CoreIdentity.Repository.DbModels;
using CoreIdentity.Repository;

namespace CoreIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var data = await GetData_Async();
            return View(data);
        }

        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> Admin()
        {
            var data = await GetData_Async();
            return View(nameof(Index), data);
        }

        private async Task<Dictionary<string, object>> GetData_Async([CallerMemberName]string methodName = null)
        {
            Dictionary<string, object> result = new Dictionary<string, object>
            {
                ["Action"] = methodName,
                ["User"] = HttpContext.User.Identity.Name,
                ["Auth Type"] = HttpContext.User.Identity.AuthenticationType
            };

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.GetUserAsync(HttpContext.User);
                var roles = await _userManager.GetRolesAsync(user);
                result["Roles"] = string.Join(", ", roles);
            }

            return result;
        }

        [HttpPost]
        public IActionResult Index(HelloModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return View("GreetUser", model);
        }
    }
}