namespace MyPerfume.Web.ViewModels.Dtos
{
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class PictureUrlDto : IMapFrom<PictureUrl>
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public string DesignerAndPerfumeNames { get; set; }

        public int PictureNumber { get; set; }
    }
}
