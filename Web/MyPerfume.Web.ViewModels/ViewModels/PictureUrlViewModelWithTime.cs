namespace MyPerfume.Web.ViewModels.ViewModels
{
    using System;

    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class PictureUrlViewModelWithTime : IMapFrom<PictureUrl>
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public bool IsSelected { get; set; }

        public string DesignerName { get; set; }

        public string PerfumeName { get; set; }

        public string AdditionalInfo { get; set; }

        public string SecondAdditionalInfo { get; set; }

        public int PictureNumber { get; set; }

        public int PictureShowNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
