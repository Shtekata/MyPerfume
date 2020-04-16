namespace MyPerfume.Web.ViewModels.Dto
{
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class DesignerDto : IMapFrom<Designer>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
