namespace MyPerfume.Web.ViewModels.InputModels
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PictureUrlInputModel
    {
        private string additionalInformation;

        public string Id { get; set; }

        public string Url
        {
            get
            {
                if (this.additionalInformation != null)
                {
                    return $"https://geshevalstorage.blob.core.windows.net/pictures/{this.DesignerName}/{this.PerfumeName}/{this.AdditionalInformation}/{this.PictureNumber}.jpg";
                }
                else
                {
                    return $"https://geshevalstorage.blob.core.windows.net/pictures/{this.DesignerName}/{this.PerfumeName}/{this.PictureNumber}.jpg";
                }
            }
        }

        public string DesignerName { get; set; }

        public string PerfumeName { get; set; }

        public string AdditionalInformation
        {
            get => this.additionalInformation;

            set => this.additionalInformation = value?.Replace(" ", string.Empty);
        }

        public int PictureNumber { get; set; }

        public int PictureShowNumber { get; set; }

        public Dictionary<string, List<SelectListItem>> Extensions { get; set; }
    }
}
