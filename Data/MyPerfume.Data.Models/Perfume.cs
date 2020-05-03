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
            this.PerfumesPictureUrls = new HashSet<PerfumePictureUrl>();
            this.PerfumesSeasons = new HashSet<PerfumeSeason>();
            this.PerfumesPurposes = new HashSet<PerfumePurpose>();
            this.PerfumesPerfumers = new HashSet<PerfumePerfumer>();
            this.PerfumesCategories = new HashSet<PerfumeCategorie>();
            this.PerfumesTopNotes = new HashSet<PerfumeTopNote>();
            this.PerfumesHeartNotes = new HashSet<PerfumeHeartNote>();
            this.PerfumesBaseNotes = new HashSet<PerfumeBaseNote>();
            this.PerfumesAromaticGroups = new HashSet<PerfumeAromaticGroup>();
            this.Products = new HashSet<Product>();
            this.Posts = new HashSet<Post>();
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

        public virtual ICollection<PerfumePictureUrl> PerfumesPictureUrls { get; set; }

        public virtual ICollection<PerfumeSeason> PerfumesSeasons { get; set; }

        public virtual ICollection<PerfumePurpose> PerfumesPurposes { get; set; }

        public virtual ICollection<PerfumePerfumer> PerfumesPerfumers { get; set; }

        public virtual ICollection<PerfumeCategorie> PerfumesCategories { get; set; }

        public virtual ICollection<PerfumeTopNote> PerfumesTopNotes { get; set; }

        public virtual ICollection<PerfumeHeartNote> PerfumesHeartNotes { get; set; }

        public virtual ICollection<PerfumeBaseNote> PerfumesBaseNotes { get; set; }

        public virtual ICollection<PerfumeAromaticGroup> PerfumesAromaticGroups { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
