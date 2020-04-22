﻿namespace MyPerfume.Web.ViewModels.Dtos
{
    using System;

    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.InputModels;
    using MyPerfume.Web.ViewModels.ViewModels;

    public class PictureUrlDto : IMapFrom<PictureUrlInputModel>, IMapTo<PictureUrlInputModel>, IMapFrom<PictureUrl>, IMapTo<PictureUrl>
    {
        public PictureUrlDto()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Url { get; set; }

        public string DesignerAndPerfumeNames { get; set; }

        public int? PictureNumber { get; set; }

        public string PerfumeId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
