namespace MyPerfume.Web.ViewModels.InputModels
{
    using AutoMapper;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class PictureUrlCollectionModel : IMapFrom<PictureUrl>, IMapFrom<Perfume>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public bool IsSelected { get; set; }

        public string DesignerAndPerfumeNames { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PictureUrl, PictureUrlCollectionModel>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.DesignerAndPerfumeNames, y => y.MapFrom(z => z.DesignerAndPerfumeNames))
                .ForMember(x => x.IsSelected, y => y.MapFrom(z => true));
        }
    }
}
