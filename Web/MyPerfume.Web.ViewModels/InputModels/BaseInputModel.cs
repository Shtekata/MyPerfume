namespace MyPerfume.Web.ViewModels.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseInputModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
