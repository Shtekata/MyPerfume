namespace MyPerfume.Web.ViewModels.Designers.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dto;

    public class CreateDesignerInputModel : IMapTo<DesignerDto>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
