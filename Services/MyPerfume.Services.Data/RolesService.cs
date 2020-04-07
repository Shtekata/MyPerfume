namespace MyPerfume.Services.Data
{
    using System.Collections;
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

        public async Task<EditRoleViewModel> EditRole(string id)
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
            return await this.roleManager.FindByIdAsync(id) == null;
        }
    }
}
