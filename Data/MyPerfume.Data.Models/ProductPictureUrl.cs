namespace MyPerfume.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class ProductPictureUrl : BaseDeletableModel<int>
    {
        [Key]
        [Required]
        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Key]
        [Required]
        public string PictureUrlId { get; set; }

        public virtual PictureUrl PictureUrl { get; set; }
    }
}
