namespace MyPerfume.Web.ViewModels.ViewModels
{
    using System.Collections.Generic;

    public class PagePictureUrlViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public ICollection<PictureUrlViewModel> PictureUrls { get; set; }
    }
}
