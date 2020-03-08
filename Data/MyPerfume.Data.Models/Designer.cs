namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyPerfume.Data.Common.Models;

    public class Designer : BaseDeletableModel<string>
    {
        public Designer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public IEnumerable<Perfume> Perfumes => new HashSet<Perfume>();
    }
}
