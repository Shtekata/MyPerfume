namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class PictureUrl : BaseDeletableModel<string>
    {
        public PictureUrl()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PerfumesPictureUrls = new HashSet<PerfumePictureUrl>();
            this.ProductsPictureUrls = new HashSet<ProductPictureUrl>();
        }

        [Required]
        [MaxLength(500)]
        public string Url { get; set; }

        [Required]
        [MaxLength(50)]
        public string DesignerName { get; set; }

        [Required]
        [MaxLength(50)]
        public string PerfumeName { get; set; }

        [MaxLength(50)]
        public string AdditionalInfo { get; set; }

        [MaxLength(50)]
        public string SecondAdditionalInfo { get; set; }

        public int PictureNumber { get; set; }

        public int PictureShowNumber { get; set; }

        public virtual ICollection<PerfumePictureUrl> PerfumesPictureUrls { get; set; }

        public virtual ICollection<ProductPictureUrl> ProductsPictureUrls { get; set; }
    }
}
