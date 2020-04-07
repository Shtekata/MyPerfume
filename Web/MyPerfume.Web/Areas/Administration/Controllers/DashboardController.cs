namespace MyPerfume.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Data;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Administration.Dashboard;
    using MyPerfume.Web.ViewModels.Administration.Roles;
    using MyPerfume.Web.ViewModels.Dto;
    using System.Threading.Tasks;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IRolesService rolesService;

        public DashboardController(ISettingsService settingsService, IRolesService rolesService)
        {
            this.settingsService = settingsService;
            this.rolesService = rolesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }

        public IActionResult CreateRole()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateRole(CreateRoleViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var roleDto = AutoMapperConfig.MapperInstance.Map<CreateRoleDto>(model);
                var result = this.rolesService.CreateRole(roleDto).Result;
                if (result.Succeeded)
                {
                    return this.RedirectToAction("AllRoles", "Dashboard"); // , new { area = string.Empty });
                }

                foreach (IdentityError error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return this.View(model);
        }

        public IActionResult AllRoles()
        {
            var roles = this.rolesService.AllRole();
            return this.View(roles);
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var roleExists = await this.rolesService.RoleExists(id);
            if (!roleExists)
            {
                this.ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found.";
            }

            var model = await this.rolesService.EditRole(id);

            return this.View(model);
        }
    }
}
