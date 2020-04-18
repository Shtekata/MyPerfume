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

    public class PictureUrlsController : BaseController
    {
        private readonly IPictureUrlsService pictureUrlsService;

        public PictureUrlsController(IPictureUrlsService pictureUrlsService)
        {
            this.pictureUrlsService = pictureUrlsService;
        }

        public IActionResult Add()
        {
            this.ViewData["ClassName"] = GlobalConstants.PictureUrlsClassName;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PictureUrlInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.PictureUrlsControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.pictureUrlsService.ExistsByName(input.Url))
            {
                return this.View("Exists");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<PictureUrlDto>(input);
            await this.pictureUrlsService.AddAsync(dto);
            return this.View("OperationIsOk");
        }

        public async Task<IActionResult> All()
        {
            this.ViewData["ClassName"] = GlobalConstants.PictureUrlsClassName;
            this.ViewData["ClassNames"] = GlobalConstants.PictureUrlsClassNames;

            var model = await this.pictureUrlsService.GetAll<PictureUrlViewModel>();

            return this.View(model);
        }

        public IActionResult Edit(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.PictureUrlsClassName;

            if (!this.pictureUrlsService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit {this.ViewData["ClassName"]} with Id : {id}!";
                return this.View("NotFound");
            }

            var dto = this.pictureUrlsService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<PictureUrlInputModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PictureUrlInputModel input)
        {
            this.ViewData["ClassName"] = GlobalConstants.PictureUrlsClassName;
            this.ViewData["ControllerName"] = GlobalConstants.PictureUrlsControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.pictureUrlsService.ExistsById(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not edit {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<PictureUrlDto>(input);
            var isTheSameInput = this.pictureUrlsService.IsTheSameInput(dto);
            if (isTheSameInput)
            {
                this.ModelState.AddModelError(string.Empty, "You mast enter a different value!");
                return this.View();
            }

            if (this.pictureUrlsService.ExistsByName(input.DesignerAndPerfumeNames))
            {
                return this.View("Exists");
            }

            var result = await this.pictureUrlsService.EditAsync(dto);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }

        public IActionResult Delete(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.PictureUrlsClassName;

            if (!this.pictureUrlsService.ExistsById(id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete {this.ViewData["ClassName"]} with Id : {id}!";
                return this.View("NotFound");
            }

            var dto = this.pictureUrlsService.GetById(id);

            var model = AutoMapperConfig.MapperInstance.Map<PictureUrlViewModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PictureUrlInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.PictureUrlsControllerName;

            if (!this.pictureUrlsService.ExistsById(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Can not delete {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("NotFound");
            }

            var result = await this.pictureUrlsService.DeleteAsync(input.Id);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit user {this.ViewData["ClassName"]} Id : {input.Id}!";
                return this.View("NotFound");
            }

            return this.View("OperationIsOk");
        }
    }
}
