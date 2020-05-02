﻿namespace MyPerfume.Web.ViewModels.ViewModels
{
    using System.Collections.Generic;

    public class PagePerfumeViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<PerfumeViewModel> Perfumes { get; set; }
    }
}