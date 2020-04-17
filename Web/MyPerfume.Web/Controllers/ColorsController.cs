﻿namespace MyPerfume.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Common;
    using MyPerfume.Services.Data;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.InputModels;
    using MyPerfume.Web.ViewModels.ViewModels;

    public class ColorsController : BaseController
    {
        private readonly IColorsService colorsService;

        public ColorsController(IColorsService colorsService)
        {
            this.colorsService = colorsService;
        }

        public IActionResult Add()
        {
            this.ViewData["ClassName"] = GlobalConstants.ColorsClassName;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(IdAndNameInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.ColorsControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.colorsService.ExistsByName(input.Name))
            {
                return this.View("Exists");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<IdAndNameDto>(input);
            await this.colorsService.AddAsync(dto);
            return this.View("OperationIsOk");
        }

        public async Task<IActionResult> All()
        {
            this.ViewData["ClassName"] = GlobalConstants.ColorsClassName;
            this.ViewData["ClassNames"] = GlobalConstants.ColorsClassNames;

            var data = await this.colorsService.GetAll<IdNameCreateModViewModel>();

            return this.View(data);
        }

        public IActionResult Edit(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.ColorsClassName;

            if (!this.colorsService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit country with Id : {id}!";
                return this.View("NotFound");
            }

            var dto = this.colorsService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<IdAndNameInputModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdAndNameInputModel input)
        {
            this.ViewData["ClassName"] = GlobalConstants.ColorsClassName;
            this.ViewData["ControllerName"] = GlobalConstants.ColorsControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.colorsService.ExistsById(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit designer with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var isTheSameInput = this.colorsService.IsTheSameInput(input);
            if (isTheSameInput)
            {
                this.ModelState.AddModelError(string.Empty, "You mast enter a different value!");
                return this.View();
            }

            var result = await this.colorsService.EditAsync(input);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit designer with Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }

        public IActionResult Delete(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.ColorsClassName;

            if (!this.colorsService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete country with Id : {id}!";
                return this.View("NotFound");
            }

            var dto = this.colorsService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<IdAndNameInputModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IdAndNameInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.ColorsControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.colorsService.ExistsById(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete country with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var result = await this.colorsService.DeleteAsync(input);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit user country Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }
    }
}