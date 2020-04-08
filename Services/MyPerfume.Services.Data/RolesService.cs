namespace MyPerfume.Services.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MyPerfume.Data.Models;
    using MyPerfume.Web.ViewModels.Administration.Roles;
    using MyPerfume.Web.ViewModels.Dto;

    public class RolesService : IRolesService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public RolesService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task AddAdmin()
        {
            var adminUser = await this.userManager.FindByNameAsync("gesheval@gmail.com");
            await this.userManager.AddToRoleAsync(adminUser, "Administrator");
        }

        public IEnumerable AllRole()
        {
            var roles = this.roleManager.Roles;
            return roles;
        }

        public async Task<IdentityResult> CreateRole(CreateRoleDto dto)
        {
            var identityRole = new ApplicationRole { Name = dto.RoleName };
            var result = await this.roleManager.CreateAsync(identityRole);
            return result;
        }

        public async Task<EditRoleViewModel> RoleWithUsers(string id)
        {
            var role = await this.roleManager.FindByIdAsync(id);

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
            };

            foreach (var user in this.userManager.Users)
            {
                if (await this.userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return model;
        }

        public async Task<bool> RoleExists(string id)
        {
            return await this.roleManager.FindByIdAsync(id) != null;
        }

        public async Task<IdentityResult> EditRole(EditRoleViewModel model)
        {
            var role = await this.roleManager.FindByIdAsync(model.Id);
            role.Name = model.RoleName;
            var result = await this.roleManager.UpdateAsync(role);
            return result;
        }

        public async Task<List<UserRoleViewModel>> UsersInRole(string roleId)
        {
            var role = await this.roleManager.FindByIdAsync(roleId);

            var model = new List<UserRoleViewModel>();

            foreach (var user in this.userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await this.userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return model;
        }

        public async Task EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await this.roleManager.FindByIdAsync(roleId);

            IdentityResult result = null;

            for (int i = 0; i < model.Count; i++)
            {
                var user = await this.userManager.FindByIdAsync(model[i].UserId);

                if (model[i].IsSelected && !(await this.userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await this.userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await this.userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await this.userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < model.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            return;
        }
    }
}
