namespace MyPerfume.Web.ViewModels.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PictureUrlInputModel
    {
        private string designerName;
        private string perfumeName;
        private string url;
        private string additionalInformation;

        public string Id { get; set; }

        [Required]
        public string Url
        {
            get
            {
                if (this.additionalInformation != null)
                {
                    return this.url = $"https://geshevalstorage.blob.core.windows.net/pictures/{this.DesignerName}/{this.PerfumeName}/{this.AdditionalInformation}/{this.PictureNumber}.jpg";
                }
                else
                {
                    return this.url = $"https://geshevalstorage.blob.core.windows.net/pictures/{this.DesignerName}/{this.PerfumeName}/{this.PictureNumber}.jpg";
                }
            }
        }

        [Required]
        public string DesignerName
        {
            get => this.designerName;

            set => this.designerName = value.Replace(" ", string.Empty);
        }

        [Required]
        public string PerfumeName
        {
            get => this.perfumeName;

            set => this.perfumeName = value.Replace(" ", string.Empty);
        }

        public string AdditionalInformation
        {
            get => this.additionalInformation;

            set => this.additionalInformation = value?.Replace(" ", string.Empty);
        }

        [Required]
        public int PictureNumber { get; set; }

        [Required]
        public int PictureShowNumber { get; set; }

        public Dictionary<string, List<SelectListItem>> Extensions { get; set; }
    }
}
