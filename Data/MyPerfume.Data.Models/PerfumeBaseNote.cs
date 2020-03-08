namespace MyPerfume.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class PerfumeBaseNote : BaseDeletableModel<int>
    {
        [Key]
        [Required]
        public string PerfumeId { get; set; }

        public Perfume Perfume { get; set; }

        [Key]
        [Required]
        public string BaseNoteId { get; set; }

        public BaseNote BaseNote { get; set; }
    }
}
