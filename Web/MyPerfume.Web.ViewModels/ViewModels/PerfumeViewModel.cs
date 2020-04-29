namespace MyPerfume.Web.ViewModels.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Ganss.XSS;
    using MyPerfume.Data.Models;
    using MyPerfume.Data.Models.Enums;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.InputModels;

    public class PerfumeViewModel : IMapFrom<Perfume>, IMapFrom<PerfumeDto>
    {
        public PerfumeViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description?.Length > 385 ? this.Description?.Substring(0, 385) + "..." : this.Description;

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public string SanitizedShortDescription => new HtmlSanitizer().Sanitize(this.ShortDescription);

        public bool Niche { get; set; }

        public int? YearOfManifacture { get; set; }

        public CustomerType CustomerType { get; set; }

        public string DesignerId { get; set; }

        public string DesignerName { get; set; }

        public string ColorId { get; set; }

        public string ColorName { get; set; }

        public string CountryId { get; set; }

        public string CountryName { get; set; }

        public string Url => $"/perfume/{this.Name.Replace(' ', '-')}";

        public int PostsCount { get; set; }

        public IList<PictureUrlCollectionModel> PictureUrls { get; set; }

        public IEnumerable<PostDto> Posts { get; set; }

        // public virtual ICollection<PerfumeSeason> PerfumesSeasons { get; set; }

        // public virtual ICollection<PerfumePurpose> PerfumesPurposes { get; set; }

        // public virtual ICollection<PerfumePerfumer> PerfumesPerfumers { get; set; }

        // public virtual ICollection<PerfumeCategorie> PerfumesCategories { get; set; }

        // public virtual ICollection<PerfumeTopNote> PerfumesTopNotes { get; set; }

        // public virtual ICollection<PerfumeHeartNote> PerfumesHeartNotes { get; set; }

        // public virtual ICollection<PerfumeBaseNote> PerfumesBaseNotes { get; set; }

        // public virtual ICollection<PerfumeAromaticGroup> PerfumesAromaticGroups { get; set; }

        // public virtual ICollection<Product> Products { get; set; }

        // public ICollection<SelectListItem> Designers { get; set; }

        // public ICollection<SelectListItem> Colors { get; set; }

        // public ICollection<SelectListItem> Countries { get; set; }

        // public DateTime CreatedOn { get; set; }

        // public DateTime ModifiedOn { get; set; }
    }
}
