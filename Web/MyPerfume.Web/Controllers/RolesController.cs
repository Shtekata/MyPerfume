using MyPerfume.Services.Data;

namespace MyPerfume.Web.Controllers
{
    public class RolesController
    {
        private readonly IRolesService rolesService;

        public RolesController(IRolesService rolesService)
        {
            this.rolesService = rolesService;
        }

        public void AddAdmin()
        {
            this.rolesService.AddAdmin();
        }
    }
}
