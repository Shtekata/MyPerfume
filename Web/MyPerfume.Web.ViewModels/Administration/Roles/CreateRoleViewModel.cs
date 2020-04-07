namespace MyPerfume.Web.ViewModels.Administration.Roles
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dto;

    public class CreateRoleViewModel : IMapTo<CreateRoleDto>
    {
        [Required]
        [MaxLength(100)]
        public string RoleName { get; set; }
    }
}
