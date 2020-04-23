namespace MyPerfume.Web.ViewModels.InputModels
{
    using System;

    using AutoMapper;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class PictureUrlCollectionModel : IMapFrom<PictureUrl>, IMapTo<PictureUrl>, IMapFrom<Perfume>, IHaveCustomMappings
    {
        public PictureUrlCollectionModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Url { get; set; }

        public bool IsSelected { get; set; }

        public string DesignerAndPerfumeNames { get; set; }

        public int PictureNumber { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PictureUrl, PictureUrlCollectionModel>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.DesignerAndPerfumeNames, y => y.MapFrom(z => z.DesignerAndPerfumeNames))
                .ForMember(x => x.IsSelected, y => y.MapFrom(z => false));
        }
    }
}
