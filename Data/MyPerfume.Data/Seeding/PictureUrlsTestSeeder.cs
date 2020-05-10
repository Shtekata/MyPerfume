namespace MyPerfume.Data.Seeding
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Blob;
    using MyPerfume.Data.Models;

    public class PictureUrlsTestSeeder : ISeeder
    {
        private StorageCredentials storageCredentials;
        private CloudStorageAccount cloudStorageAccount;
        private CloudBlobClient cloudBlobClient;
        private CloudBlobContainer cloudBlobContainer;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            this.storageCredentials = new StorageCredentials(configuration["BlobStorageName"], configuration["BlobKey"]);
            this.cloudStorageAccount = new CloudStorageAccount(this.storageCredentials, true);
            this.cloudBlobClient = this.cloudStorageAccount.CreateCloudBlobClient();
            this.cloudBlobContainer = this.cloudBlobClient.GetContainerReference("pictures");

            if (dbContext.PictureUrls.Any())
            {
                return;
            }

            var fileNamePaths = Directory.EnumerateFiles(@"D:\OneDrive\Documents\MyDocuments\Website\Pictures");
            foreach (var fileNamePath in fileNamePaths)
            {
                var fileName = Path.GetFileName(fileNamePath);
                var blobUrl = $"Asen/Gesho/{fileName}";
                var pictureUrl = $"https://geshevalstorage.blob.core.windows.net/pictures/{blobUrl}";

                CloudBlockBlob blockBlob = this.cloudBlobContainer.GetBlockBlobReference(blobUrl);
                var stream = File.OpenRead(fileNamePath);
                blockBlob.Properties.ContentType = "image/jpg";
                await blockBlob.UploadFromStreamAsync(stream);
                await dbContext.PictureUrls.AddAsync(new PictureUrl
                {
                    Url = pictureUrl,
                    DesignerName = "Asen",
                    PerfumeName = "Gesho",
                    PictureNumber = int.Parse(Regex.Match(fileName, @"[\d]+").Value),
                });
            }
        }
    }
}
