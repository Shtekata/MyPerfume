namespace MyPerfume.Web.ViewModels.ViewModels
{
    using System.Collections.Generic;

    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;

    public class PagePerfumeViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<PerfumeViewModel> Perfumes { get; set; }
    }
}
