namespace MyPerfume.Web.ViewModels.Perfums.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyPerfume.Data.Models;
    using MyPerfume.Data.Models.Enums;

    public class CreatePerfumInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        public bool Niche { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Year Of Manifacture")]
        public int? YearOfManifacture { get; set; }

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

        public IEnumerable<SelectListItem> Designers { get; set; }

        public IEnumerable<SelectListItem> Colors { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
