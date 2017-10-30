using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using CoreIdentity.Repository.DbModels;
using CoreIdentity.Models.Admin;
using Microsoft.EntityFrameworkCore;

namespace CoreIdentity.Controllers
{
    public class RoleAdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleAdminController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));

                if (result.Succeeded)
                    return RedirectToAction(nameof(RoleAdminController.Index));
                else
                    AddErrorsFromResult(result);
            }

            return View((object)name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                    return RedirectToAction(nameof(RoleAdminController.Index));
            }
            return View("Index", _roleManager.Roles);
        }

        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            IList<AppUser> nonMembers = new List<AppUser>();
            IList<AppUser> allUsers = await _userManager.Users.ToListAsync();
            var memebers = (await _userManager.GetUsersInRoleAsync(role.Name)).ToDictionary(u => u.Id, u => u);

            foreach (AppUser user in allUsers)
            {
                if (!memebers.ContainsKey(user.Id))
                    nonMembers.Add(user);
            }

            var model = new RoleEditModel
            {
                Role = role,
                Members = memebers.Values,
                NonMembers = nonMembers
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModifyModel model)
        {
            IdentityResult result;

            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                            AddErrorsFromResult(result);
                    }
                }

                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                            AddErrorsFromResult(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            return await Edit(model.RoleId);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}