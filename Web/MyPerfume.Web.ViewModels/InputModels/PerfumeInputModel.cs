namespace MyPerfume.Web.ViewModels.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyPerfume.Data.Models.Enums;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;

    public class PerfumeInputModel : IMapFrom<PerfumeDto>
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        public bool Niche { get; set; }

        [Display(Name = "Year Of Manifacture")]
        public int YearOfManifacture { get; set; }

        [Required]
        [Display(Name = "Customer Type")]
        public CustomerType CustomerType { get; set; }

        [Required]
        [Display(Name = "Designer")]
        public string DesignerId { get; set; }

        [Display(Name = "Color")]
        public string ColorId { get; set; }

        [Display(Name = "Country")]
        public string CountryId { get; set; }

        [Display(Name = "Picture")]
        public string PictureUrlId { get; set; }

        public IList<PictureUrlCollectionModel> PictureUrls { get; set; }

        public Dictionary<string, List<SelectListItem>> Extensions { get; set; }
    }
}
