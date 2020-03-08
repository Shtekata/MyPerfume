﻿namespace MyPerfume.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

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
    }
}
