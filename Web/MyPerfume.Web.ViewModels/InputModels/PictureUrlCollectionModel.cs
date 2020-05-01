﻿namespace MyPerfume.Web.ViewModels.InputModels
{
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class PictureUrlCollectionModel : IMapFrom<PictureUrl>, IMapTo<PictureUrl>, IMapFrom<Perfume>
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public bool IsSelected { get; set; }

        public string DesignerAndPerfumeNames { get; set; }

        public int PictureNumber { get; set; }

        public int PictureShowNumber { get; set; }
    }
}
