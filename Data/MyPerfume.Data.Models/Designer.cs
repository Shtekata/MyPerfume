namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
 
    using MyPerfume.Data.Common.Models;
    using MyPerfume.Services.Mapping;

    public class Designer : BaseDeletableModel<string>
    {
        public Designer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Perfumes = new HashSet<Perfume>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual IEnumerable<Perfume> Perfumes { get; set; }
    }
}
