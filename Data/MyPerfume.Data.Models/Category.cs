namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public IEnumerable<PerfumeCategorie> PerfumesCategories => new HashSet<PerfumeCategorie>();
    }
}
