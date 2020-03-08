﻿namespace MyPerfume.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class PerfumeHeartNote : BaseDeletableModel<int>
    {
        [Key]
        [Required]
        public string PerfumeId { get; set; }

        public Perfume Perfume { get; set; }

        [Key]
        [Required]
        public string HeartNoteId { get; set; }

        public HeartNote HeartNote { get; set; }
    }
}
