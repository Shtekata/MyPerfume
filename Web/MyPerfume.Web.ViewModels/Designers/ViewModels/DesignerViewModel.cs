namespace MyPerfume.Web.ViewModels.Designers.ViewModels
{
    using System;

    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class DesignerViewModel : IMapFrom<Designer>
    {
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
