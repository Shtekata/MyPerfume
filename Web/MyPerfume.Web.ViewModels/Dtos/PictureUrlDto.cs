namespace MyPerfume.Web.ViewModels.Dtos
{
    using System;

    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.InputModels;
    using MyPerfume.Web.ViewModels.ViewModels;

    public class PictureUrlDto : IMapFrom<PictureUrlInputModel>, IMapTo<PictureUrlInputModel>, IMapTo<PictureUrlViewModel>, IMapTo<PictureUrlViewModelWithTime>, IMapFrom<PictureUrl>, IMapTo<PictureUrl>
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public bool IsSelected { get; set; }

        public string DesignerName { get; set; }

        public string PerfumeName { get; set; }

        public int PictureNumber { get; set; }

        public int PictureShowNumber { get; set; }

        public string PerfumeId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
