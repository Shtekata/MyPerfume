namespace MyPerfume.Web.ViewModels.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PictureUrlInputModel
    {
        private string designerAndPerfumeNames;

        public PictureUrlInputModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Url => $"https://geshevalstorage.blob.core.windows.net/pictures/{this.DesignerAndPerfumeNames}/{this.PictureNumber}.jpg";

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string DesignerAndPerfumeNames
        {
            get => this.designerAndPerfumeNames;

            set => this.designerAndPerfumeNames = value.Replace(" ", string.Empty);
        }

        [Required]
        [Range(1, 10000)]
        public int PictureNumber { get; set; }

        public List<SelectListItem> PictureNumbers { get; set; }
    }
}
