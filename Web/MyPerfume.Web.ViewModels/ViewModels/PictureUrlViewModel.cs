namespace MyPerfume.Web.ViewModels.ViewModels
{
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;

    public class PictureUrlViewModel : IMapFrom<PictureUrlDto>, IMapTo<PictureUrlDto>, IMapFrom<PerfumePictureUrl>, IMapTo<PerfumePictureUrl>
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public bool IsSelected { get; set; }

        public string DesignerName { get; set; }

        public string PerfumeName { get; set; }

        public string AdditionalInfo { get; set; }

        public string SecondAdditionalInfo { get; set; }

        public int PictureNumber { get; set; }

        public int PictureShowNumber { get; set; }
    }
}
