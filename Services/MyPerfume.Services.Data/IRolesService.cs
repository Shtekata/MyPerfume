namespace MyPerfume.Services.Data
{
    using System.Collections;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MyPerfume.Web.ViewModels.Administration.Roles;
    using MyPerfume.Web.ViewModels.Dto;

    public interface IRolesService
    {
        Task<IdentityResult> CreateRole(CreateRoleDto dto);

        IEnumerable AllRole();

        Task<bool> RoleExists(string id);

        Task<EditRoleViewModel> EditRole(string id);

        Task AddAdmin();
    }
}
