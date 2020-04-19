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

    public class PerfumersController : BaseController
    {
        private readonly IPerfumersService perfumersService;

        public PerfumersController(IPerfumersService perfumersService)
        {
            this.perfumersService = perfumersService;
        }

        public IActionResult Add()
        {
            this.ViewData["ClassName"] = GlobalConstants.PerfumersClassName;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(BaseInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.PerfumersControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.perfumersService.ExistsByName(input.Name))
            {
                return this.View("Exists");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<BaseDto>(input);
            await this.perfumersService.AddAsync(dto);
            return this.View("OperationIsOk");
        }

        public async Task<IActionResult> All()
        {
            this.ViewData["ClassName"] = GlobalConstants.PerfumersClassName;
            this.ViewData["ClassNames"] = GlobalConstants.PerfumersClassNames;

            var model = await this.perfumersService.GetAll<BaseViewModel>();

            return this.View(model);
        }

        public IActionResult Edit(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.PerfumersClassName;

            if (!this.perfumersService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit {this.ViewData["ClassName"]} with Id : {id}!";
                return this.View("NotFound");
            }

            var dto = this.perfumersService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<BaseInputModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BaseInputModel input)
        {
            this.ViewData["ClassName"] = GlobalConstants.PerfumersClassName;
            this.ViewData["ControllerName"] = GlobalConstants.PerfumersControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.perfumersService.ExistsById(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<BaseDto>(input);
            var isTheSameInput = this.perfumersService.IsTheSameInput(dto);
            if (isTheSameInput)
            {
                this.ModelState.AddModelError(string.Empty, "You mast enter a different value!");
                return this.View();
            }

            if (this.perfumersService.ExistsByName(input.Name))
            {
                return this.View("Exists");
            }

            var result = await this.perfumersService.EditAsync(dto);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }

        public IActionResult Delete(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.PerfumersClassName;

            if (!this.perfumersService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete {this.ViewData["ClassName"]} with Id : {id}!";
                return this.View("NotFound");
            }

            var dto = this.perfumersService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<BaseViewModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BaseInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.PerfumersControllerName;

            if (!this.perfumersService.ExistsById(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var result = await this.perfumersService.DeleteAsync(input.Id);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit user {this.ViewData["ClassName"]} Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }
    }
}