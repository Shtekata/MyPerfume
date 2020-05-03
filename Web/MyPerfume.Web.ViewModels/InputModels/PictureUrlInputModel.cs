namespace MyPerfume.Web.ViewModels.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PictureUrlInputModel
    {
        private string designerName;
        private string perfumeName;

        public string Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Url => $"https://geshevalstorage.blob.core.windows.net/pictures/{this.DesignerName}/{this.PerfumeName}/{this.PictureNumber}.jpg";

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string DesignerName
        {
            get => this.designerName;

            set => this.designerName = value.Replace(" ", string.Empty);
        }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string PerfumeName
        {
            get => this.perfumeName;

            set => this.perfumeName = value.Replace(" ", string.Empty);
        }

        public int PictureNumber { get; set; }

        public int PictureShowNumber { get; set; }

        public List<SelectListItem> PictureNumbers { get; set; }

        public List<SelectListItem> PictureShowNumbers { get; set; }
    }
}
