namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class TopNote : BaseDeletableModel<string>
    {
        public TopNote()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual IEnumerable<PerfumeTopNote> PerfumesTopNotes => new HashSet<PerfumeTopNote>();
    }
}
