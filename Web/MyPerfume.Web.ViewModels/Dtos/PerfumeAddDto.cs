namespace MyPerfume.Web.ViewModels.Dtos
{
    using MyPerfume.Data.Models;
    using MyPerfume.Data.Models.Enums;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.InputModels;

    public class PerfumeAddDto : IMapFrom<PerfumeInputModel>, IMapTo<Perfume>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Niche { get; set; }

        public int YearOfManifacture { get; set; }

        public CustomerType CustomerType { get; set; }

        public string DesignerId { get; set; }

        public string ColorId { get; set; }

        public string CountryId { get; set; }
    }
}
