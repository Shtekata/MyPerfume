namespace MyPerfume.Web.ViewModels.Dto
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Models.Enums;

    public class PerfumDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Niche { get; set; }

        public int? YearOfManifacture { get; set; }

        public CustomerType CustomerType { get; set; }

        public string DesignerId { get; set; }

        public string ColorId { get; set; }

        public string CountryId { get; set; }
    }
}
