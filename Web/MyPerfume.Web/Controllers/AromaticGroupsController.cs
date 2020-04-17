namespace MyPerfume.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Common;
    using MyPerfume.Services.Data;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.InputModels;
    using MyPerfume.Web.ViewModels.ViewModels;

    public class AromaticGroupsController : BaseController
    {
        private readonly IAromaticGroupsService aromaticGroupsService;

        public AromaticGroupsController(IAromaticGroupsService aromaticGroupsService)
        {
            this.aromaticGroupsService = aromaticGroupsService;
        }

        public IActionResult Add()
        {
            this.ViewData["ClassName"] = GlobalConstants.AromaticGroupsClassName;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(IdAndNameInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.AromaticGroupsControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.aromaticGroupsService.ExistsByName(input.Name))
            {
                return this.View("Exists");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<IdAndNameDto>(input);
            await this.aromaticGroupsService.AddAsync(dto);
            return this.View("OperationIsOk");
        }

        public async Task<IActionResult> All()
        {
            this.ViewData["ClassName"] = GlobalConstants.AromaticGroupsClassName ;
            this.ViewData["ClassNames"] = GlobalConstants.AromaticGroupsClassNames;

            var data = await this.aromaticGroupsService.GetAll<IdNameCreateModViewModel>();

            return this.View(data);
        }

        public IActionResult Edit(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.AromaticGroupsClassName;

            if (!this.aromaticGroupsService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit country with Id : {id}!";
                return this.View("NotFound");
            }

            var dto = this.aromaticGroupsService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<IdAndNameInputModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdAndNameInputModel input)
        {
            this.ViewData["ClassName"] = GlobalConstants.AromaticGroupsClassName;
            this.ViewData["ControllerName"] = GlobalConstants.AromaticGroupsControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.aromaticGroupsService.ExistsById(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit designer with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var isTheSameInput = this.aromaticGroupsService.IsTheSameInput(input);
            if (isTheSameInput)
            {
                this.ModelState.AddModelError(string.Empty, "You mast enter a different value!");
                return this.View();
            }

            var result = await this.aromaticGroupsService.EditAsync(input);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit designer with Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }

        public IActionResult Delete(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.AromaticGroupsClassName;

            if (!this.aromaticGroupsService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete country with Id : {id}!";
                return this.View("NotFound");
            }

            var dto = this.aromaticGroupsService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<IdAndNameInputModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IdAndNameInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.AromaticGroupsControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.aromaticGroupsService.ExistsById(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete country with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var result = await this.aromaticGroupsService.DeleteAsync(input);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit user country Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }
    }
}
