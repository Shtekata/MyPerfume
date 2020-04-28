namespace MyPerfume.Web.ViewModels.ViewModels
{
    using System.Collections.Generic;

    public class PageViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public ICollection<PerfumeViewModel> Perfumes { get; set; }
    }
}
