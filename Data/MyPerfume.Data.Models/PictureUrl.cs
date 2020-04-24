namespace MyPerfume.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class PictureUrl : BaseDeletableModel<string>
    {
        public PictureUrl()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(500)]
        public string Url { get; set; }

        [Required]
        [MaxLength(100)]
        public string DesignerAndPerfumeNames { get; set; }

        public int PictureNumber { get; set; }

        public int PictureShowNumber { get; set; }

        public string PerfumeId { get; set; }

        public virtual Perfume Perfume { get; set; }

        public string ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
