namespace MyPerfume.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Services.Data;
    using MyPerfume.Web.ViewModels.Dto;
    using MyPerfume.Web.ViewModels.Perfums.InputModels;

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

            var designerDto = new DesignerDto { Name = input.Name };
            if (this.designerService.Exists(designerDto))
            {
                return this.Redirect("/Designers/Exists");
            }

            await this.designerService.AddAsync(designerDto);
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
    }
}
