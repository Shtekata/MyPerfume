namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyPerfume.Data.Common.Models;

    public class HeartNote : BaseDeletableModel<string>
    {
        public HeartNote()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public ICollection<PerfumeHeartNote> PerfumesHeartNotes => new HashSet<PerfumeHeartNote>();
    }
}
