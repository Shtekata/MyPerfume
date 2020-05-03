namespace MyPerfume.Web.ViewModels.ViewModels
{
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class PerfumePictureUrlViewModel : IMapFrom<PerfumePictureUrl>
    {
        public string PerfumeId { get; set; }

        public Perfume Perfume { get; set; }

        public string PictureUrlId { get; set; }

        public PictureUrl PictureUrl { get; set; }
    }
}
