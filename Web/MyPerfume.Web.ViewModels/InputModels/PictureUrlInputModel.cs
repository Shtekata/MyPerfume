namespace MyPerfume.Web.ViewModels.InputModels
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PictureUrlInputModel
    {
        private string additionalInfo;
        private string secondAdditionalInfo;

        public string Id { get; set; }

        public string Url
        {
            get
            {
                if (this.additionalInfo == null && this.secondAdditionalInfo != null)
                {
                    this.additionalInfo = this.secondAdditionalInfo;
                    this.secondAdditionalInfo = null;
                }

                if (this.additionalInfo != null && this.secondAdditionalInfo != null)
                {
                    return $"https://geshevalstorage.blob.core.windows.net/pictures/{this.DesignerName}/{this.PerfumeName}/{this.AdditionalInfo}/{this.SecondAdditionalInfo}/{this.PictureNumber}.jpg";
                }
                else if (this.additionalInfo != null && this.secondAdditionalInfo == null)
                {
                    return $"https://geshevalstorage.blob.core.windows.net/pictures/{this.DesignerName}/{this.PerfumeName}/{this.AdditionalInfo}/{this.PictureNumber}.jpg";
                }
                else
                {
                    return $"https://geshevalstorage.blob.core.windows.net/pictures/{this.DesignerName}/{this.PerfumeName}/{this.PictureNumber}.jpg";
                }
            }
        }

        public string DesignerName { get; set; }

        public string PerfumeName { get; set; }

        public string AdditionalInfo
        {
            get => this.additionalInfo;

            set => this.additionalInfo = value?.Replace(" ", string.Empty);
        }

        public string SecondAdditionalInfo
        {
            get => this.secondAdditionalInfo;

            set => this.secondAdditionalInfo = value?.Replace(" ", string.Empty);
        }

        public int PictureNumber { get; set; }

        public int PictureShowNumber { get; set; }

        public Dictionary<string, List<SelectListItem>> Extensions { get; set; }
    }
}
