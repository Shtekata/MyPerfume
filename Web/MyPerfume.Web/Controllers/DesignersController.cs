namespace MyPerfume.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Services.Data;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Designers.InputModels;
    using MyPerfume.Web.ViewModels.Designers.ViewModels;
    using MyPerfume.Web.ViewModels.Dto;

    public class DesignersController : BaseController
    {
        private readonly IDesignersService designerService;

        public DesignersController(IDesignersService designerService)
        {
            this.designerService = designerService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateDesignerInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.designerService.Exists(input))
            {
                return this.Redirect("/Designers/Exists");
            }

            var dtoDesigner = AutoMapperConfig.MapperInstance.Map<DesignerDto>(input);
            await this.designerService.AddAsync(dtoDesigner);
            return this.Redirect("/Designers/OperationIsOk");
        }

        public IActionResult Exists()
        {
            return this.View();
        }

        public IActionResult OperationIsOk()
        {
            return this.View();
        }

        public async Task<IActionResult> All()
        {
            var allDesigners = await this.designerService.GetAllDesigners<DesignerViewModel>();

            return this.View(allDesigners);
        }
    }
}
