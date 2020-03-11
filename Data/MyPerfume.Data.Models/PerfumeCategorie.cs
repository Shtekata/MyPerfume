namespace MyPerfume.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class PerfumeCategorie : BaseDeletableModel<int>
    {
        [Key]
        [Required]
        public string PerfumeId { get; set; }

        public virtual Perfume Perfume { get; set; }

        [Key]
        [Required]
        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
