namespace MyPerfume.Web.ViewModels.ViewModels
{
    using System.Collections.Generic;

    using MyPerfume.Data.Models;
    using MyPerfume.Data.Models.Enums;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;

    public class PerfumeViewModel : IMapFrom<Perfume>, IMapFrom<PerfumeDto>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Niche { get; set; }

        public int? YearOfManifacture { get; set; }

        public virtual CustomerType CustomerType { get; set; }

        public string DesignerId { get; set; }

        public string DesignerName { get; set; }

        public string ColorId { get; set; }

        public string ColorName { get; set; }

        public string CountryId { get; set; }

        public string CountryName { get; set; }

        public virtual IEnumerable<string> PictureUrlsUrl { get; set; }

        // public virtual IEnumerable<PerfumeSeason> PerfumesSeasons { get; set; }

        // public virtual IEnumerable<PerfumePurpose> PerfumesPurposes { get; set; }

        // public virtual IEnumerable<PerfumePerfumer> PerfumesPerfumers { get; set; }

        // public virtual IEnumerable<PerfumeCategorie> PerfumesCategories { get; set; }

        // public virtual IEnumerable<PerfumeTopNote> PerfumesTopNotes { get; set; }

        // public virtual IEnumerable<PerfumeHeartNote> PerfumesHeartNotes { get; set; }

        // public virtual IEnumerable<PerfumeBaseNote> PerfumesBaseNotes { get; set; }

        // public virtual IEnumerable<PerfumeAromaticGroup> PerfumesAromaticGroups { get; set; }

        // public virtual ICollection<Product> Products { get; set; }

        // public IEnumerable<SelectListItem> Designers { get; set; }

        // public IEnumerable<SelectListItem> Colors { get; set; }

        // public IEnumerable<SelectListItem> Countries { get; set; }

        // public DateTime CreatedOn { get; set; }

        // public DateTime ModifiedOn { get; set; }
    }
}
