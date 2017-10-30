using CoreIdentity.Repository.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Core.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleUsersTagHelper : TagHelper
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public RoleUsersTagHelper(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HtmlAttributeName("identity-role")]
        public string RoleId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();
            IdentityRole role = await _roleManager.FindByIdAsync(RoleId);

            if (role != null)
                names = (await _userManager.GetUsersInRoleAsync(role.Name)).Select(u => u.UserName).ToList();

            output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(", ", names));
        }
    }
}
