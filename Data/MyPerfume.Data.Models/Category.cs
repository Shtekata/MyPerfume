namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyPerfume.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public ICollection<PerfumeCategorie> PerfumesCategories => new HashSet<PerfumeCategorie>();
    }
}
