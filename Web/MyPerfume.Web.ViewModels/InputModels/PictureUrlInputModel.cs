namespace MyPerfume.Web.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class PictureUrlInputModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Url => $"https://geshevalstorage.blob.core.windows.net/pictures/{this.DesignerAndPerfumeNames}.jpg";

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string DesignerAndPerfumeNames { get; set; }

        [Required]
        [Range(1, 10000)]
        public int PictureNumber { get; set; }
    }
}
