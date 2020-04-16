namespace MyPerfume.Web.ViewModels.Designers.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dto;

    public class DesignerInputModel : IMapTo<DesignerDto>, IMapFrom<DesignerDto>
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
