namespace MyPerfume.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Common;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Data;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.InputModels;
    using MyPerfume.Web.ViewModels.ViewModels;

    public class PerfumesController : BaseController
    {
        private const int ItemsPerPage = 5;

        private readonly IPerfumesService perfumesService;
        private readonly IPictureUrlsService pictureUrlsService;

        public PerfumesController(IPerfumesService perfumesService, IPictureUrlsService pictureUrlsService)
        {
            this.perfumesService = perfumesService;
            this.pictureUrlsService = pictureUrlsService;
        }

        public async Task<IActionResult> Add()
        {
            this.ViewData["ClassName"] = GlobalConstants.PerfumesClassName;

            var model = new PerfumeInputModel();
            model.Extensions = await this.perfumesService.Extensions();
            model.PictureUrls = this.pictureUrlsService.GetPerfumePictures<PictureUrlCollectionModel>();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PerfumeInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.PerfumesControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.perfumesService.ExistsByName(input.Name))
            {
                return this.View("Exists");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<PerfumeDto>(input);
            var perfumeDto = await this.perfumesService.AddAsync(dto);

            var result = await this.pictureUrlsService.EditAsync(perfumeDto);
            if (result == 0)
            {
                this.ViewData["errormessage"] = $"can not add {this.ViewData["classname"]} with id : {input.Id}!";
                return this.View("Error");
            }

            return this.View("OperationIsOk");
        }

        public async Task<IActionResult> All(int id = 1)
        {
            this.ViewData["ClassName"] = GlobalConstants.PerfumesClassName;
            this.ViewData["ClassNames"] = GlobalConstants.PerfumesClassNames;

            // var model = await this.perfumesService.GetAll<PerfumeViewModel>();
            var count = this.perfumesService.GetCount();
            var model = new PagePerfumeViewModel
            {
                PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage),
                Perfumes = await this.perfumesService.GetPage<PerfumeViewModel>(ItemsPerPage, (id - 1) * ItemsPerPage),
            };

            if (model.PagesCount == 0)
            {
                model.PagesCount = 1;
            }

            model.CurrentPage = id;

            return this.View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.PerfumesClassName;

            if (!this.perfumesService.ExistsById(id))
            {
                this.ViewData["NotFoundMessage"] = $"Item with this Id : {id} is not exists!";
                return this.View("NotFound");
            }

            var dto = this.perfumesService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<PerfumeInputModel>(dto);
            model.Extensions = await this.perfumesService.Extensions();
            model.PictureUrls = this.pictureUrlsService.GetPerfumePictures<PictureUrlCollectionModel>();
            foreach (var pictureUrl in model.PictureUrls)
            {
                if (this.pictureUrlsService.GetByPerfumeAndPictureUrlId(id, pictureUrl.Id))
                {
                    pictureUrl.IsSelected = true;
                }
                else
                {
                    pictureUrl.IsSelected = false;
                }
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PerfumeInputModel input)
        {
            this.ViewData["ClassName"] = GlobalConstants.PerfumesClassName;
            this.ViewData["ControllerName"] = GlobalConstants.PerfumesControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.perfumesService.ExistsById(input.Id))
            {
                this.ViewData["NotFoundMessage"] = $"Item with this Id : {input.Id} is not exists!";
                return this.View("NotFound");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<PerfumeDto>(input);

            var isTheSameInput = this.perfumesService.IsTheSameInput(dto);
            if (isTheSameInput)
            {
                this.ModelState.AddModelError(string.Empty, "You mast enter a different value!");
                input.Extensions = await this.perfumesService.Extensions();
                return this.View(input);
            }

            var result1 = await this.pictureUrlsService.EditAsync(dto);
            var result2 = await this.perfumesService.EditAsync(dto);

            if (result1 == 0 && result2 == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("Error");
            }

            return this.View("OperationIsOk");
        }

        public IActionResult Delete(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.PerfumesClassName;

            if (!this.perfumesService.ExistsById(id))
            {
                this.ViewData["NotFoundMessage"] = $"Item with this Id : {id} is not exists!";
                return this.View("NotFound");
            }

            var dto = this.perfumesService.GetById(id);

            var model = AutoMapperConfig.MapperInstance.Map<PerfumeViewModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PerfumeInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.PerfumesControllerName;

            if (!this.perfumesService.ExistsById(input.Id))
            {
                this.ViewData["NotFoundMessage"] = $"Item with this Id : {input.Id} is not exists!";
                return this.View("NotFound");
            }

            var result = await this.perfumesService.DeleteAsync(input.Id);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not delete {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("Error");
            }

            return this.View("OperationIsOk");
        }

        public IActionResult ByName(string name)
        {
            var perfumDto = this.perfumesService.GetByName<PerfumeDto>(name);
            var viewModel = AutoMapperConfig.MapperInstance.Map<PerfumeViewModel>(perfumDto);
            return this.View(viewModel);
        }
    }
}
