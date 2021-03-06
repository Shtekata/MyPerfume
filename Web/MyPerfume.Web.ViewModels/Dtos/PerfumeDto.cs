﻿namespace MyPerfume.Web.ViewModels.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyPerfume.Data.Models;
    using MyPerfume.Data.Models.Enums;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.InputModels;
    using MyPerfume.Web.ViewModels.ViewModels;

    public class PerfumeDto : IMapFrom<Perfume>, IMapTo<Perfume>, IMapFrom<PerfumeInputModel>, IMapTo<PerfumeViewModel>, IHaveCustomMappings
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

        public string PictureUrlId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int PostsCount { get; set; }

        public IEnumerable<PictureUrlDto> PictureUrls { get; set; }

        public IEnumerable<PerfumeTopNote> TopNotes { get; set; }

        public IEnumerable<string> HeartNotes { get; set; }

        public IEnumerable<string> BaseNotes { get; set; }

        public IEnumerable<string> Perfumers { get; set; }

        public IEnumerable<string> AromaticGroups { get; set; }

        // public IEnumerable<string> PerfumesSeasons { get; set; }

        // public IEnumerable<string> PerfumesPurposes { get; set; }
        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<PostDto> Posts { get; set; }

        public Dictionary<string, List<SelectListItem>> Extensions { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Perfume, PerfumeDto>().ForMember(
                m => m.PictureUrls,
                opt => opt.MapFrom(z => z.PerfumesPictureUrls.Select(x => new PictureUrlDto
                {
                    Id = x.PictureUrlId,
                    DesignerName = x.PictureUrl.DesignerName,
                    PerfumeName = x.PictureUrl.PerfumeName,
                    AdditionalInfo = x.PictureUrl.AdditionalInfo,
                    SecondAdditionalInfo = x.PictureUrl.SecondAdditionalInfo,
                    PictureNumber = x.PictureUrl.PictureNumber,
                    PictureShowNumber = x.PictureUrl.PictureShowNumber,
                    Url = x.PictureUrl.Url,
                })));
        }
    }
}
