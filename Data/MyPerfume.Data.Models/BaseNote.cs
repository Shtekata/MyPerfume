namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyPerfume.Data.Common.Models;

    public class BaseNote : BaseDeletableModel<string>
    {
        public BaseNote()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public ICollection<PerfumeBaseNote> PerfumesBaseNotes => new HashSet<PerfumeBaseNote>();
    }
}
