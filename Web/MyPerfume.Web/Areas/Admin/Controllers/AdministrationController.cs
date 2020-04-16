namespace MyPerfume.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Common;
    using MyPerfume.Services.Data;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.Controllers;
    using MyPerfume.Web.ViewModels.Administration.Roles;
    using MyPerfume.Web.ViewModels.Administration.Users;
    using MyPerfume.Web.ViewModels.Dto;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + "Admin")]
    [Authorize]
    [Area("Admin")]
    public class AdministrationController : BaseController
    {
        private readonly IRolesService rolesService;
        private readonly IUsersService usersService;

        public AdministrationController(IRolesService rolesService, IUsersService usersService)
        {
            this.rolesService = rolesService;
            this.usersService = usersService;
        }

        public IActionResult AllUsers()
        {
            var users = this.usersService.All<ListOfUsersViewModel>();

            return this.View(users);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var userExists = await this.usersService.UserExists(id);
            if (!userExists)
            {
                this.ViewData["ErrorMessage"] = $"User with Id ={id} cannot be found.";
                return this.View("NotFound");
            }

            var model = this.usersService.GetUserById<EditUserViewModel>(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel input)
        {
            var userExists = await this.usersService.UserExists(input.Id);
            if (!userExists)
            {
                this.ViewData["ErrorMessage"] = $"User with Id ={input.Id} cannot be found.";
                return this.View("NotFound");
            }

            var userDto = AutoMapperConfig.MapperInstance.Map<EditUserDto>(input);
            await this.usersService.EditUser(userDto);

            return this.RedirectToAction("Success", new { id = input.Id });
        }

        public async Task<IActionResult> ConfirmDeleteUser(string id)
        {
            var userExists = await this.usersService.UserExists(id);
            if (!userExists)
            {
                this.ViewData["ErrorMessage"] = $"User with Id ={id} cannot be found.";
                return this.View("NotFound");
            }

            var model = this.usersService.GetUserById<EditUserViewModel>(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteUser(EditUserViewModel input)
        {
            var userExists = await this.usersService.UserExists(input.Id);
            if (!userExists)
            {
                this.ViewData["ErrorMessage"] = $"User with Id ={input.Id} cannot be found.";
                return this.View("NotFound");
            }

            var result = await this.usersService.DeleteUserById(input.Id);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not delete user with Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.RedirectToAction("AllUsers");
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
                    return this.RedirectToAction("AllRoles"); // , new { area = string.Empty });
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
                return this.View("NotFound");
            }

            var model = await this.rolesService.RoleWithUsers(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var roleExists = await this.rolesService.RoleExists(model.Id);
            if (!roleExists)
            {
                this.ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found.";
                return this.View("NotFound");
            }

            var result = await this.rolesService.EditRole(model);

            if (result.Succeeded)
            {
                return this.RedirectToAction("AllRoles");
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return this.View(model);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var roleExists = await this.rolesService.RoleExists(id);
            if (!roleExists)
            {
                this.ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found.";
                return this.View("NotFound");
            }

            var model = await this.rolesService.RoleWithUsers(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(EditRoleViewModel model)
        {
            var roleExists = await this.rolesService.RoleExists(model.Id);
            if (!roleExists)
            {
                this.ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found.";
                return this.View("NotFound");
            }
            else
            {
                var result = await this.rolesService.DeleteRole(model);

                if (result.Succeeded)
                {
                    return this.RedirectToAction("AllRoles");
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }

                return this.Redirect("AllRoles");
            }
        }

        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            this.ViewData["RoleId"] = roleId;

            var roleExists = await this.rolesService.RoleExists(roleId);
            if (!roleExists)
            {
                this.ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found.";
                return this.View("NotFound");
            }

            var model = await this.rolesService.UsersInRole(roleId);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var roleExists = await this.rolesService.RoleExists(roleId);
            if (!roleExists)
            {
                this.ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found.";
                return this.View("NotFound");
            }

            await this.rolesService.EditUsersInRole(model, roleId);

            return this.RedirectToAction("EditRole", new { Id = roleId });
        }

        public IActionResult Success(string id)
        {
            return this.View("Success", id);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return this.View();
        }
    }
}
