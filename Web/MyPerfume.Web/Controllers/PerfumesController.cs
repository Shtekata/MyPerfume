namespace MyPerfume.Web.Controllers
{
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
            await this.perfumesService.AddAsync(dto);
            return this.View("OperationIsOk");
        }

        public async Task<IActionResult> All()
        {
            this.ViewData["ClassName"] = GlobalConstants.PerfumesClassName;
            this.ViewData["ClassNames"] = GlobalConstants.PerfumesClassNames;

            var model = await this.perfumesService.GetAll<PerfumeViewModel>();

            return this.View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.PerfumesClassName;

            if (!this.perfumesService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit {this.ViewData["ClassName"]} with Id : {id}!";
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
                this.ViewData["ErrorMessage"] = $"Can not edit {this.ViewData["ClassName"]} with Id : {input.Id}!";
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
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }

        public IActionResult Delete(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.PerfumesClassName;

            if (!this.perfumesService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete {this.ViewData["ClassName"]} with Id : {id}!";
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
                this.ViewData["ErrorMessage"] = $"Can not delete {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var result = await this.perfumesService.DeleteAsync(input.Id);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit user {this.ViewData["ClassName"]} Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }
    }
}
