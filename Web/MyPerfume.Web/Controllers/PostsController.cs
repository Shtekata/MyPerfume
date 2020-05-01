namespace MyPerfume.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyPerfume.Common;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Data;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.InputModels;
    using MyPerfume.Web.ViewModels.ViewModels;

    [Authorize]
    public class PostsController : BaseController
    {
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(IPostsService postsService, UserManager<ApplicationUser> userManager)
        {
            this.postsService = postsService;
            this.userManager = userManager;
        }

        public IActionResult Create(string id, string name)
        {
            this.ViewData["PerfumeName"] = name;

            var model = new PostInputModel
            {
                PerfumeName = name,
                PerfumeId = id,
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostInputModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.PostsControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.postsService.ExistsByTitle(input.Title))
            {
                return this.View("Exists");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<PostDto>(input);
            var user = await this.userManager.GetUserAsync(this.User);
            dto.UserId = user.Id;
            var result = await this.postsService.AddAsync(dto);
            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not add {this.ViewData["ClassName"]} with Id : {input.Title}!";
                return this.View("Error");
            }

            return this.Redirect($"/perfume/{input.PerfumeName}");
        }

        public async Task<IActionResult> All()
        {
            this.ViewData["ClassName"] = GlobalConstants.PostsClassName;
            this.ViewData["ClassNames"] = GlobalConstants.PostsClassNames;

            var model = await this.postsService.GetAll<BaseViewModel>();

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + "Admin")]
        public IActionResult Edit(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.PostsClassName;

            if (!this.postsService.ExistsById(id))
            {
                this.ViewData["NotFoundMessage"] = $"Item with this Id : {id} is not exists!";
                return this.View("NotFound");
            }

            var dto = this.postsService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<BaseInputModel>(dto);

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(BaseInputModel input)
        {
            this.ViewData["ClassName"] = GlobalConstants.PostsClassName;
            this.ViewData["ControllerName"] = GlobalConstants.PostsControllerName;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.postsService.ExistsById(input.Id))
            {
                this.ViewData["NotFoundMessage"] = $"Item with this Id : {input.Id} is not exists!";
                return this.View("NotFound");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<PostDto>(input);
            var isTheSameInput = this.postsService.IsTheSameInput(dto);
            if (isTheSameInput)
            {
                this.ModelState.AddModelError(string.Empty, "You mast enter a different value!");
                return this.View();
            }

            if (this.postsService.ExistsByTitle(input.Name))
            {
                return this.View("Exists");
            }

            var result = await this.postsService.EditAsync(dto);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not edit {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("Error");
            }

            return this.View("OperationIsOk");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + "Admin")]
        public IActionResult Delete(string id)
        {
            this.ViewData["ClassName"] = GlobalConstants.PostsClassName;

            if (!this.postsService.ExistsById(id))
            {
                this.ViewData["NotFoundMessage"] = $"Item with this Id : {id} is not exists!";
                return this.View("NotFound");
            }

            var dto = this.postsService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<BaseViewModel>(dto);

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(BaseViewModel input)
        {
            this.ViewData["ControllerName"] = GlobalConstants.PostsControllerName;

            if (!this.postsService.ExistsById(input.Id))
            {
                this.ViewData["NotFoundMessage"] = $"Item with this Id : {input.Id} is not exists!";
                return this.View("NotFound");
            }

            var result = await this.postsService.DeleteAsync(input.Id);

            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not delete {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("Error");
            }

            return this.View("OperationIsOk");
        }
    }
}
