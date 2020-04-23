namespace MyPerfume.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Net.Http.Headers;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Blob;
    using MyPerfume.Common;
    using MyPerfume.Services.Data;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.InputModels;
    using MyPerfume.Web.ViewModels.ViewModels;

    public class PictureUrlsController : BaseController
    {
        private readonly IPictureUrlsService pictureUrlsService;
        private readonly IWebHostEnvironment env;
        private StorageCredentials storageCredentials;
        private CloudStorageAccount cloudStorageAccount;
        private CloudBlobClient cloudBlobClient;
        private CloudBlobContainer cloudBlobContainer;
        private IWebHostEnvironment hostingEnvironment;

        public PictureUrlsController(
            IPictureUrlsService pictureUrlsService,
            IWebHostEnvironment env,
            IConfiguration configuration)
        {
            this.pictureUrlsService = pictureUrlsService;
            this.env = env;
            this.storageCredentials = new StorageCredentials(configuration["BlobStorageName"], configuration["BlobKey"]);
            this.cloudStorageAccount = new CloudStorageAccount(this.storageCredentials, true);
            this.cloudBlobClient = this.cloudStorageAccount.CreateCloudBlobClient();
            this.cloudBlobContainer = this.cloudBlobClient.GetContainerReference("pictures");
        }

        public IActionResult Add()
        {
            this.ViewData["ClassName"] = GlobalConstants.PictureUrlsClassName;

            var model = new PictureUrlInputModel();
            model.PictureNumbers = this.pictureUrlsService.PictureNumbers();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PictureUrlInputModel input, IList<IFormFile> files)
        {
            this.ViewData["ControllerName"] = GlobalConstants.PictureUrlsControllerName;

            input.PictureNumbers = this.pictureUrlsService.PictureNumbers();
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.pictureUrlsService.ExistsByUrl(input.Url))
            {
                return this.View("Exists");
            }

            var pictureName = input.Url.Replace("https://geshevalstorage.blob.core.windows.net/pictures/", string.Empty);
            await this.UploadFiles(files, pictureName);

            var dto = AutoMapperConfig.MapperInstance.Map<PictureUrlDto>(input);

            var result = await this.pictureUrlsService.AddAsync(dto);
            if (result == 0)
            {
                this.ViewData["ErrorMessage"] = $"Can not add {this.ViewData["ClassName"]} with Id : {input.Id}!";
                return this.View("NotFound");
            }

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
                this.ViewData["ErrorMessage"] = $"Item with this Id : {id} is not exists!";
                return this.View("NotFound");
            }

            var dto = this.pictureUrlsService.GetById(id);
            var model = AutoMapperConfig.MapperInstance.Map<PictureUrlInputModel>(dto);
            model.PictureNumbers = this.pictureUrlsService.PictureNumbers();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PictureUrlInputModel input)
        {
            this.ViewData["ClassName"] = GlobalConstants.PictureUrlsClassName;
            this.ViewData["ControllerName"] = GlobalConstants.PictureUrlsControllerName;
            input.PictureNumbers = this.pictureUrlsService.PictureNumbers();

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.pictureUrlsService.ExistsById(input.Id))
            {
                this.ViewData["ErrorMessage"] = $"Item with this Id : {input.Id} is not exists!";
                return this.View("NotFound");
            }

            if (this.pictureUrlsService.ExistsByUrl(input.DesignerAndPerfumeNames))
            {
                return this.View("Exists");
            }

            var dto = AutoMapperConfig.MapperInstance.Map<PictureUrlDto>(input);
            var isTheSameInput = this.pictureUrlsService.IsTheSameInput(dto);
            if (isTheSameInput)
            {
                this.ModelState.AddModelError(string.Empty, "You mast enter a different value!");
                return this.View(input);
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

        public async Task<IActionResult> Blobs()
        {
            this.ViewData["ContainerName"] = "Name : " + this.cloudBlobContainer.Name;
            await this.GetAllBlobs();
            return this.View();
        }

        public async Task<IActionResult> UploadFiles(IList<IFormFile> files, string pictureName)
        {
            long size = 0;
            var fileSizes = new List<long>();
            try
            {
                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue
                        .Parse(file.ContentDisposition)
                        .FileName
                        .Trim();

                    CloudBlockBlob blockBlob = this.cloudBlobContainer.GetBlockBlobReference(pictureName);
                    var stream = file.OpenReadStream();
                    size = file.Length;
                    fileSizes.Add(size);
                    blockBlob.Properties.ContentType = "image/jpg";
                    await blockBlob.UploadFromStreamAsync(stream);
                }

                this.ViewData["FileSizes"] = fileSizes;
            }
            catch (Exception)
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.Json("Upload Failed. Please try again.");
            }

            return this.Ok();
        }

        private async Task GetAllBlobs()
        {
            BlobContinuationToken continuationToken = null;
            CloudBlob blob;

            int? segmentSize = null;
            var resultSegment = await this.cloudBlobContainer.ListBlobsSegmentedAsync(string.Empty, true, BlobListingDetails.Metadata, segmentSize, continuationToken, null, null);

            var blobs = new List<CloudBlob>();
            foreach (var blobItem in resultSegment.Results)
            {
                blob = (CloudBlob)blobItem;
                blobs.Add(blob);
            }

            this.ViewData["Blobs"] = blobs;
        }
    }
}
