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

    public class HeartNotesController : BaseController
    {
        private readonly IHeartNotesService heartNotesService;

        public HeartNotesController(IHeartNotesService heartNotesService)
        {
            this.heartNotesService = heartNotesService;
        }

        public IActionResult Add()
        {
            this.ViewData["ClassName"] = GlobalConstants.HeartNotesClassName;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(IdAndNameInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.HeartNotesControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.heartNotesService.ExistsByName(input.Name))
            {
                return this.View("Exists");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<IdAndNameDto>(input);
            await this.heartNotesService.AddAsync(dto);
            return this.View("OperationIsOk");
        }

        public async Task<IActionResult> All()
        {
            this.ViewData["ClassName"] = GlobalConstants.HeartNotesClassName;
            this.ViewData["ClassNames"] = GlobalConstants.HeartNotesClassNames;

            var data = await this.heartNotesService.GetAll<IdNameCreateModViewModel>();

            return this.View(data);
        }

        public IActionResult Edit(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.HeartNotesClassName;

            if (!this.heartNotesService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit country with Id : {id}!";
                return this.View("NotFound");
            }

            var dto = this.heartNotesService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<IdAndNameInputModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdAndNameInputModel input)
        {
            this.ViewData["ClassName"] = GlobalConstants.HeartNotesClassName;
            this.ViewData["ControllerName"] = GlobalConstants.HeartNotesControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.heartNotesService.ExistsById(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit designer with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var isTheSameInput = this.heartNotesService.IsTheSameInput(input);
            if (isTheSameInput)
            {
                this.ModelState.AddModelError(string.Empty, "You mast enter a different value!");
                return this.View();
            }

            var result = await this.heartNotesService.EditAsync(input);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit designer with Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }

        public IActionResult Delete(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.HeartNotesClassName;

            if (!this.heartNotesService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete country with Id : {id}!";
                return this.View("NotFound");
            }

            var dto = this.heartNotesService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<IdAndNameInputModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IdAndNameInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.HeartNotesControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.heartNotesService.ExistsById(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete country with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var result = await this.heartNotesService.DeleteAsync(input);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit user country Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }
    }
}
