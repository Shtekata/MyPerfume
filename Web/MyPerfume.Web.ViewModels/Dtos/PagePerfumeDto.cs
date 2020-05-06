namespace MyPerfume.Web.ViewModels.Dtos
{
    using System.Collections.Generic;

    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.ViewModels;

    public class PagePerfumeDto : IMapTo<PagePerfumeViewModel>
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<PerfumeDto> Perfumes { get; set; }
    }
}
