namespace MyPerfume.Web.ViewModels.Perfums.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateDesignerInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
