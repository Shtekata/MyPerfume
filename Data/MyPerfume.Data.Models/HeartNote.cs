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
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public IEnumerable<PerfumeHeartNote> PerfumesHeartNotes => new HashSet<PerfumeHeartNote>();
    }
}
