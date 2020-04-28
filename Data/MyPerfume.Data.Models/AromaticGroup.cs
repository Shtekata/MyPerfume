namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class AromaticGroup : BaseDeletableModel<string>
    {
        public AromaticGroup()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PerfumesAromaticGroups = new HashSet<PerfumeAromaticGroup>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<PerfumeAromaticGroup> PerfumesAromaticGroups { get; set; }
    }
}
