namespace MyPerfume.Web.ViewModels.Dtos
{
    using System;
    using System.Collections.Generic;

    using MyPerfume.Data.Models;
    using MyPerfume.Data.Models.Enums;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.InputModels;

    public class PerfumeDto : IMapFrom<PerfumeInputModel>, IMapTo<PerfumeInputModel>, IMapFrom<Perfume>, IMapTo<Perfume>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Niche { get; set; }

        public int YearOfManifacture { get; set; }

        public CustomerType CustomerType { get; set; }

        public string DesignerId { get; set; }

        public string DesignerName { get; set; }

        public string ColorId { get; set; }

        public string ColorName { get; set; }

        public string CountryId { get; set; }

        public string CountryName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public IEnumerable<string> TopNotes { get; set; }

        public IEnumerable<string> HeartNotes { get; set; }

        public IEnumerable<string> BaseNotes { get; set; }

        public IEnumerable<string> Perfumers { get; set; }

        public IEnumerable<string> AromaticGroups { get; set; }

        public IEnumerable<string> PictureUrls { get; set; }

        public IEnumerable<string> PerfumesSeasons { get; set; }

        public IEnumerable<string> PerfumesPurposes { get; set; }

        public IEnumerable<string> Categories { get; set; }
    }
}
