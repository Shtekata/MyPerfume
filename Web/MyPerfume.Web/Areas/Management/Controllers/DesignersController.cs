namespace MyPerfume.Web.Areas.Management.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Common;
    using MyPerfume.Services.Data;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.Controllers;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.InputModels;
    using MyPerfume.Web.ViewModels.ViewModels;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + "Admin")]
    [Authorize]
    [Area("Management")]
    public class DesignersController : BaseController
    {
        private readonly IDesignersService designersService;

        public DesignersController(IDesignersService designersService)
        {
            this.designersService = designersService;
        }

        public IActionResult Add()
        {
            this.ViewData["ClassName"] = GlobalConstants.DesignersClassName;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(BaseInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.DesignersControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.designersService.ExistsByName(input.Name))
            {
                return this.View("Exists");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<BaseDto>(input);
            var result = await this.designersService.AddAsync(dto);
            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not add {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("Error");
            }

            return this.View("OperationIsOk");
        }

        public async Task<IActionResult> All()
        {
            this.ViewData["ClassName"] = GlobalConstants.DesignersClassName;
            this.ViewData["ClassNames"] = GlobalConstants.DesignersClassNames;

            var model = await this.designersService.GetAll<BaseViewModel>();

            return this.View(model);
        }

        public IActionResult Edit(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.DesignersClassName;

            if (!this.designersService.ExistsById(id))
            {
                this.ViewData["NotFoundMessage"] = $"Item with this Id : {id} is not exists!";
                return this.View("NotFound");
            }

            var dto = this.designersService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<BaseInputModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BaseInputModel input)
        {
            this.ViewData["ClassName"] = GlobalConstants.DesignersClassName;
            this.ViewData["ControllerName"] = GlobalConstants.DesignersControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.designersService.ExistsById(input.Id))
            {
                this.ViewData["NotFoundMessage"] = $"Item with this Id : {input.Id} is not exists!";
                return this.View("NotFound");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<BaseDto>(input);
            var isTheSameInput = this.designersService.IsTheSameInput(dto);
            if (isTheSameInput)
            {
                this.ModelState.AddModelError(string.Empty, "You mast enter a different value!");
                return this.View();
            }

            if (this.designersService.ExistsByName(input.Name))
            {
                return this.View("Exists");
            }

            var result = await this.designersService.EditAsync(dto);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("Error");
            }

            return this.View("OperationIsOk");
        }

        public IActionResult Delete(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.DesignersClassName;

            if (!this.designersService.ExistsById(id))
            {
                this.ViewData["NotFoundMessage"] = $"Item with this Id : {id} is not exists!";
                return this.View("NotFound");
            }

            var dto = this.designersService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<BaseViewModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BaseViewModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.DesignersControllerName;

            if (!this.designersService.ExistsById(input.Id))
            {
                this.ViewData["NotFoundMessage"] = $"Item with this Id : {input.Id} is not exists!";
                return this.View("NotFound");
            }

            var result = await this.designersService.DeleteAsync(input.Id);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not delete {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("Error");
            }

            return this.View("OperationIsOk");
        }
    }
}
