namespace MyPerfume.Web.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;

    public class IdAndNameInputModel : IMapTo<IdAndNameDto>, IMapFrom<IdAndNameDto>
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
