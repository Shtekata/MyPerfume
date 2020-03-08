namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyPerfume.Data.Common.Models;

    public class TopNote : BaseDeletableModel<string>
    {
        public TopNote()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public ICollection<PerfumeTopNote> PerfumesTopNotes => new HashSet<PerfumeTopNote>();
    }
}
