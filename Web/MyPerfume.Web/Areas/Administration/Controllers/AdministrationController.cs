namespace MyPerfume.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Common;
    using MyPerfume.Web.Controllers;

    //[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Authorize]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
