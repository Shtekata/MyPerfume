namespace MyPerfume.Web.ViewModels.ViewModels
{
    using System.Collections.Generic;

    public class PagePictureUrlViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<PictureUrlViewModel> PictureUrls { get; set; }
    }
}
