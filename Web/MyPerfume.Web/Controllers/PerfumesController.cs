namespace MyPerfume.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyPerfume.Services.Data;
    using MyPerfume.Web.ViewModels.Dto;
    using MyPerfume.Web.ViewModels.Perfums.InputModels;

    public class PerfumesController : BaseController
    {
        private readonly IPerfumesService perfumeService;

        public PerfumesController(IPerfumesService perfumeService)
        {
            this.perfumeService = perfumeService;
        }

        public IActionResult Add()
        {
            var designers = this.perfumeService.GetAllDesigners().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            });

            var colors = this.perfumeService.GetAllColors().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            });

            var countries = this.perfumeService.GetAllCountries().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            });

            var model = new CreatePerfumInputModel
            {
                Designers = designers,
                Colors = colors,
                Countries = countries,
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreatePerfumInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Designers = this.perfumeService.GetAllDesigners().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
                input.Colors = this.perfumeService.GetAllColors().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
                input.Countries = this.perfumeService.GetAllCountries().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

                return this.View(input);
            }

            var perfumDto = new PerfumDto
            {
                Name = input.Name,
                Description = input.Description,
                Niche = input.Niche,
                YearOfManifacture = input.YearOfManifacture,
                CustomerType = input.CustomerType,
                DesignerId = input.DesignerId,
                ColorId = input.ColorId,
                CountryId = input.CountryId,
            };

            await this.perfumeService.AddAsync(perfumDto);
            return this.Redirect("/");
        }
    }
}
