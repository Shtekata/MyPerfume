namespace MyPerfume.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class PerfumePictureUrl : BaseDeletableModel<int>
    {
        [Key]
        [Required]
        public string PerfumeId { get; set; }

        public virtual Perfume Perfume { get; set; }

        [Key]
        [Required]
        public string PictureUrlId { get; set; }

        public virtual PictureUrl PictureUrl { get; set; }
    }
}
