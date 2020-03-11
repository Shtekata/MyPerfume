namespace MyPerfume.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class PerfumeTopNote : BaseDeletableModel<string>
    {
        [Key]
        [Required]
        public string PerfumeId { get; set; }

        public virtual Perfume Perfume { get; set; }

        [Key]
        [Required]
        public string TopNoteId { get; set; }

        public virtual TopNote TopNote { get; set; }
    }
}
