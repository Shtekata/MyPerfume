namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;
    using MyPerfume.Data.Models.Enums;

    public class Product : BaseDeletableModel<string>
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string PerfumeId { get; set; }

        public Perfume Perfume { get; set; }

        [MaxLength(5000)]
        public string Description { get; set; }

        [Range(1, 500)]
        public int Quantity { get; set; }

        [Range(0, 5000)]
        public decimal Price { get; set; }

        [Required]
        public PerfumeType PerfumeType { get; set; }

        [Range(1800, 2200)]
        public int Year { get; set; }

        public bool InStock { get; set; }

        public IEnumerable<PictureUrl> PictureUrls => new List<PictureUrl>();

        //public ICollection<Sale> Sales => new HashSet<Sale>();
    }
}
