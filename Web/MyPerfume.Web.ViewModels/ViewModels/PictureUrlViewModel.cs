namespace MyPerfume.Web.ViewModels.ViewModels
{
    using System;

    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;

    public class PictureUrlViewModel : IMapFrom<PictureUrl>, IMapFrom<PictureUrlDto>
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public string DesignerAndPerfumeNames { get; set; }

        public int PictureNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
