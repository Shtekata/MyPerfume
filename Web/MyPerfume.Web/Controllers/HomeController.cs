namespace MyPerfume.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Common;
    using MyPerfume.Services.Data;
    using MyPerfume.Web.ViewModels;
    using MyPerfume.Web.ViewModels.ViewModels;

    public class HomeController : BaseController
    {
        private readonly IPerfumesService perfumesService;

        public HomeController(IPerfumesService perfumesService)
        {
            this.perfumesService = perfumesService;
        }

        public async Task<IActionResult> Index()
        {
            this.ViewData["HomeWelcome"] = GlobalConstants.HomeWelcome;

            var model = await this.perfumesService.GetAll<PerfumeViewModel>();

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
