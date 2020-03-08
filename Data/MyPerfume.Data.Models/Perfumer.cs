namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyPerfume.Data.Common.Models;

    public class Perfumer : BaseDeletableModel<string>
    {
        public Perfumer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public IEnumerable<PerfumePerfumer> PerfumesPerfumers => new HashSet<PerfumePerfumer>();
    }
}
