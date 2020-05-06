namespace MyPerfume.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Common;
    using MyPerfume.Services.Data;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.ViewModels;

    public class HomeController : BaseController
    {
        private const int ItemsPerPage = 5;

        private readonly IPerfumesService perfumesService;

        public HomeController(IPerfumesService perfumesService)
        {
            this.perfumesService = perfumesService;
        }

        public async Task<IActionResult> Index(int id = 1)
        {
            this.ViewData["HomeWelcome"] = GlobalConstants.HomeWelcome;

            var count = this.perfumesService.GetCount();
            var modelDto = new PagePerfumeDto
            {
                PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage),
                Perfumes = await this.perfumesService.GetPage<PerfumeDto>(ItemsPerPage, (id - 1) * ItemsPerPage),
            };

            var model = AutoMapperConfig.MapperInstance.Map<PagePerfumeViewModel>(modelDto);

            if (model.PagesCount == 0)
            {
                model.PagesCount = 1;
            }

            model.CurrentPage = id;

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
