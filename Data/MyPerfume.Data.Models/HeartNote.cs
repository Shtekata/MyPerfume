namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class HeartNote : BaseDeletableModel<string>
    {
        public HeartNote()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PerfumesHeartNotes = new HashSet<PerfumeHeartNote>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<PerfumeHeartNote> PerfumesHeartNotes { get; set; }
    }
}
