namespace MyPerfume.Web.ViewModels.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class PictureUrlServiceModel : IMapFrom<Perfume>, IHaveCustomMappings
    {
        public IEnumerable<string> PictureUrls { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Perfume, PictureUrlServiceModel>().ForMember(
               m => m.PictureUrls,
               opt => opt.MapFrom(x => x.PictureUrls.Select(y => y.Id)));
        }
    }
}
