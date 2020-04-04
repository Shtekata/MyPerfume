namespace MyPerfume.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;
    using MyPerfume.Data.Models.Enums;

    public class Perfume : BaseDeletableModel<string>
    {
        public Perfume()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PictureUrls = new HashSet<PictureUrl>();
            this.PerfumesSeasons = new HashSet<PerfumeSeason>();
            this.PerfumesPurposes = new HashSet<PerfumePurpose>();
            this.PerfumesPerfumers = new HashSet<PerfumePerfumer>();
            this.PerfumesCategories = new HashSet<PerfumeCategorie>();
            this.PerfumesHeartNotes = new HashSet<PerfumeHeartNote>();
            this.PerfumesBaseNotes = new HashSet<PerfumeBaseNote>();
            this.PerfumesAromaticGroups = new HashSet<PerfumeAromaticGroup>();
            this.Products = new HashSet<Product>();
        }

        [Required]
        public string Name { get; set; }

        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        public bool Niche { get; set; }

        public int? YearOfManifacture { get; set; }

        [Required]
        public virtual CustomerType CustomerType { get; set; }

        [Required]
        public string DesignerId { get; set; }

        public virtual Designer Designer { get; set; }

        public string ColorId { get; set; }

        public virtual Color Color { get; set; }

        public string CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual IEnumerable<PictureUrl> PictureUrls { get; set; }

        public virtual IEnumerable<PerfumeSeason> PerfumesSeasons { get; set; }

        public virtual IEnumerable<PerfumePurpose> PerfumesPurposes { get; set; }

        public virtual IEnumerable<PerfumePerfumer> PerfumesPerfumers { get; set; }

        public virtual IEnumerable<PerfumeCategorie> PerfumesCategories { get; set; }

        public virtual IEnumerable<PerfumeHeartNote> PerfumesHeartNotes { get; set; }

        public virtual IEnumerable<PerfumeBaseNote> PerfumesBaseNotes { get; set; }

        public virtual IEnumerable<PerfumeAromaticGroup> PerfumesAromaticGroups { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
