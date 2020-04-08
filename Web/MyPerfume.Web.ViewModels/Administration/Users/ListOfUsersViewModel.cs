namespace MyPerfume.Web.ViewModels.Administration.Users
{
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class ListOfUsersViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
