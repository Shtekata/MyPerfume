namespace MyPerfume.Web.ViewModels.Designers.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class CreateDesignerInputModel : IMapTo<Designer>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
