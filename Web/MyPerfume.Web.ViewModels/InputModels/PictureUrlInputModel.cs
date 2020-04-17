namespace MyPerfume.Web.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;

    public class PictureUrlInputModel : IMapFrom<PictureUrlDto>, IMapTo<PictureUrlDto>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Url { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string DesignerAndPerfumeNames { get; set; }

        [Required]
        [Range(1, 10000)]
        public int PictureNumber { get; set; }
    }
}
