namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class BaseNote : BaseDeletableModel<string>
    {
        public BaseNote()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PerfumesBaseNotes = new HashSet<PerfumeBaseNote>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual IEnumerable<PerfumeBaseNote> PerfumesBaseNotes { get; set; }
    }
}
