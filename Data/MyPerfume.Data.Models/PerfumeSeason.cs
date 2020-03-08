namespace MyPerfume.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;
    using MyPerfume.Data.Models.Enums;

    public class PerfumeSeason : BaseDeletableModel<string>
    {
        [Key]
        [Required]
        public string PerfumeId { get; set; }

        public Perfume Perfume { get; set; }

        [Key]
        [Required]
        public string SeasonId { get; set; }

        public Season Season { get; set; }
    }
}
