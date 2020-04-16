﻿namespace MyPerfume.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Services.Data;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.InputModels;
    using MyPerfume.Web.ViewModels.ViewModels;

    public class CountriesController : BaseController
    {
        private readonly ICountriesService countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(IdAndNameInputModel input)
        {
            this.ViewData["ControllerName"] = "Countries";

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.countriesService.ExistsByName(input.Name))
            {
                return this.View("Exists");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<IdAndNameDto>(input);
            await this.countriesService.AddAsync(dto);
            return this.View("OperationIsOk");
        }

        public async Task<IActionResult> All()
        {
            var data = await this.countriesService.GetAll<IdNameCreateModViewModel>();

            return this.View(data);
        }

        public IActionResult Edit(string id)
        {
            if (!this.countriesService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit country with Id : {id}!";
                return this.View("NotFound");
            }

            var dto = this.countriesService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<IdAndNameInputModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdAndNameInputModel input)
        {
            this.ViewData["ControllerName"] = "Countries";

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.countriesService.ExistsById(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit country with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var result = await this.countriesService.EditAsync(input);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit country with Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }

        public IActionResult Delete(string id)
        {
            if (!this.countriesService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete country with Id : {id}!";
                return this.View("NotFound");
            }

            var dto = this.countriesService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<IdAndNameInputModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IdAndNameInputModel input)
        {
            this.ViewData["ControllerName"] = "Designers";

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.countriesService.ExistsById(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete country with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var result = await this.countriesService.DeleteAsync(input);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit user country Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }
    }
}