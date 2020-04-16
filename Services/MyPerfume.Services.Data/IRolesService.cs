namespace MyPerfume.Services.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MyPerfume.Web.ViewModels.Administration.Roles;
    using MyPerfume.Web.ViewModels.Dtos;

    public interface IRolesService
    {
        Task<IdentityResult> CreateRole(CreateRoleDto dto);

        IEnumerable AllRole();

        Task<bool> RoleExists(string id);

        Task<EditRoleViewModel> RoleWithUsers(string id);

        Task<IdentityResult> EditRole(EditRoleViewModel model);

        Task<IdentityResult> DeleteRole(EditRoleViewModel model);

        Task<List<UserRoleViewModel>> UsersInRole(string roleId);

        Task EditUsersInRole(List<UserRoleViewModel> model, string roleId);

        Task AddAdmin();
    }
}
