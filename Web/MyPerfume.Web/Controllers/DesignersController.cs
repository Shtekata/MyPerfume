namespace MyPerfume.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Services.Data;
    using MyPerfume.Web.ViewModels.Designers.InputModels;
    using MyPerfume.Web.ViewModels.Designers.ViewModels;

    public class DesignersController : BaseController
    {
        private readonly IDesignerService designerService;

        public DesignersController(IDesignerService designerService)
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
                return this.View();
            }

            if (this.designerService.Exists(input))
            {
                return this.Redirect("/Designers/Exists");
            }

            await this.designerService.AddAsync(input);
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
