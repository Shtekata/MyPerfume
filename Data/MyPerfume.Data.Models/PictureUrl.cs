﻿namespace MyPerfume.Data.Models
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
        }

        [Required]
        [MaxLength(500)]
        public string Url { get; set; }

        public string PerfumeId { get; set; }

        public Perfume Perfume { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }
    }
}