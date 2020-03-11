namespace MyPerfume.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class PerfumePerfumer : BaseDeletableModel<int>
    {
        [Key]
        [Required]
        public string PerfumeId { get; set; }

        public virtual Perfume Perfume { get; set; }

        [Key]
        [Required]
        public string PerfumerId { get; set; }

        public virtual Perfumer Perfumer { get; set; }
    }
}
