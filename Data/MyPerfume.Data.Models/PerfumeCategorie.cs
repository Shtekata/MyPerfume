namespace MyPerfume.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class PerfumeCategorie : BaseDeletableModel<int>
    {
        [Key]
        [Required]
        public string PerfumeId { get; set; }

        public Perfume Perfume { get; set; }

        [Key]
        [Required]
        public string CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
