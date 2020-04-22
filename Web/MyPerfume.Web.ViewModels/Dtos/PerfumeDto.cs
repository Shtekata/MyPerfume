﻿namespace MyPerfume.Web.ViewModels.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MyPerfume.Data.Models;
    using MyPerfume.Data.Models.Enums;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.InputModels;

    public class PerfumeDto : IMapFrom<PerfumeInputModel>, IMapFrom<Perfume>, IMapTo<Perfume>, IHaveCustomMappings
    {
        public PerfumeDto()
        {
            this.Id = Guid.NewGuid().ToString();
        }

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

        public string PictureUrlId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public virtual IEnumerable<PictureUrlCollectionModel> PictureUrls { get; set; }

        public IEnumerable<string> TopNotes { get; set; }

        public IEnumerable<string> HeartNotes { get; set; }

        public IEnumerable<string> BaseNotes { get; set; }

        public IEnumerable<string> Perfumers { get; set; }

        public IEnumerable<string> AromaticGroups { get; set; }

        public IEnumerable<string> PerfumesSeasons { get; set; }

        public IEnumerable<string> PerfumesPurposes { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Perfume, PerfumeDto>()
                .ForMember(x => x.PictureUrls, y => y.MapFrom(z => z.PictureUrls.Select(v => new PictureUrlCollectionModel
                {
                    Id = v.Id,
                    DesignerAndPerfumeNames = v.DesignerAndPerfumeNames,
                    IsSelected = true,
                })))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name));
        }
    }
}
