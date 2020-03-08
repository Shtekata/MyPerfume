namespace MyPerfume.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;
    using MyPerfume.Data.Models.Enums;

    public class Perfume : BaseDeletableModel<string>
    {
        public Perfume()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Name { get; set; }

        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(500)]
        public string PictureUrl { get; set; }

        [Required]
        public bool Niche { get; set; }

        public int? YearOfManifacture { get; set; }

        [Required]
        public CustomerType CustomerType { get; set; }

        [Required]
        public string DesignerId { get; set; }

        public Designer Designer { get; set; }

        public string ColorId { get; set; }

        public Color Color { get; set; }

        public string CountryId { get; set; }

        public Country Country { get; set; }
    }
}
