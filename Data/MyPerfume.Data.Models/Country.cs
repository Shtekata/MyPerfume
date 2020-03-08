namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyPerfume.Data.Common.Models;

    public class Country : BaseDeletableModel<string>
    {
        public Country()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public ICollection<Perfume> Perfumes => new HashSet<Perfume>();
    }
}
