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
        public async Task<IActionResult> Add(DesignerInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.designerService.Exists(input.Id))
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

        public IActionResult Edit(string id)
        {
            if (!this.designerService.Exists(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit user with Id : {id}!";
                return this.View("NotFound");
            }

            var designerDto = this.designerService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<DesignerInputModel>(designerDto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DesignerInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.designerService.Exists(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit user with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var result = await this.designerService.EditAsync(input);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit user with Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.Redirect("/Designers/OperationIsOk");
        }

        public IActionResult Delete(string id)
        {
            if (!this.designerService.Exists(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete user with Id : {id}!";
                return this.View("NotFound");
            }

            var designerDto = this.designerService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<DesignerInputModel>(designerDto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DesignerInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.designerService.Exists(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete user with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var result = await this.designerService.DeleteAsync(input);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit user with Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.Redirect("/Designers/OperationIsOk");
        }
    }
}
