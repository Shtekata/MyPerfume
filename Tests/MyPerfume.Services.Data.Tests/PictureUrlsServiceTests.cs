namespace MyPerfume.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MyPerfume.Data;
    using MyPerfume.Data.Common.Repositories;
    using MyPerfume.Data.Models;
    using MyPerfume.Data.Repositories;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.InputModels;
    using Xunit;

    public class PictureUrlsServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<PictureUrl>>();
            repository.Setup(r => r.All()).Returns(new List<PictureUrl>
                                                        {
                                                            new PictureUrl(),
                                                            new PictureUrl(),
                                                            new PictureUrl(),
                                                        }.AsQueryable());
            var service = new PictureUrlsService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PictureUrlsTest1Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PicturesUrls.Add(new PictureUrl());
            dbContext.PicturesUrls.Add(new PictureUrl());
            dbContext.PicturesUrls.Add(new PictureUrl());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository);
            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest2Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "A", });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "B", });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository);
            var result = service.ExistsById("A");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest3Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "A", });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "B", });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository);
            var result = service.ExistsById("D");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByUrlShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest4Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "A", Url = "E" });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "B", Url = "F" });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "C", Url = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository);
            var result = service.ExistsByUrl("E");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByUrlShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest5Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "A", Url = "E" });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "B", Url = "F" });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "C", Url = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository);
            var result = service.ExistsByUrl("H");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<int> EditAsyncShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest6Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "A", Url = "E", PerfumeId = "1" });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "B", Url = "F", PerfumeId = "2" });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "C", Url = "G", PerfumeId = "3" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository);
            var result = await service.EditAsync(new PerfumeDto
            {
                Id = "3",
                PictureUrls = new List<PictureUrlCollectionModel>
                {
                    new PictureUrlCollectionModel
                    {
                        Id = "A",
                        Url = "E",
                    },
                },
            });

            Assert.Equal(1, result);
            return result;
        }

        [Fact]
        public async Task<int> EditAsyncShouldReturnFalseWithIncorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest7Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "A", Url = "E", PerfumeId = "1" });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "B", Url = "F", PerfumeId = "2" });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "C", Url = "G", PerfumeId = "3" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository);
            var result = await service.EditAsync(new PerfumeDto
            {
                Id = "3",
                PictureUrls = new List<PictureUrlCollectionModel>
                {
                    new PictureUrlCollectionModel
                    {
                        Id = "D",
                        Url = "E",
                    },
                },
            });

            Assert.Equal(0, result);
            return result;
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnTrueWithCorrectInputIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PictureUrlsTest8Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "A", });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "B", });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository);
            var result = await service.DeleteAsync("A");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalseWithIncorrectInputIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PictureUrlsTest9Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "A", });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "B", });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository);
            var result = await service.DeleteAsync("D");

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task<bool> IsTheSameInputShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest10Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "A", DesignerAndPerfumeNames = "E" });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "B", DesignerAndPerfumeNames = "F" });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "C", DesignerAndPerfumeNames = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository);
            var input = new PictureUrlDto
            {
                Id = "A",
                DesignerAndPerfumeNames = "E",
            };
            var result = service.IsTheSameInput(input);

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> IsTheSameInputShouldReturnFalseWithIncorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest11Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "A", DesignerAndPerfumeNames = "E" });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "B", DesignerAndPerfumeNames = "F" });
            dbContext.PicturesUrls.Add(new PictureUrl { Id = "C", DesignerAndPerfumeNames = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository);
            var input = new PictureUrlDto
            {
                Id = "A",
                DesignerAndPerfumeNames = "H",
            };
            var result = service.IsTheSameInput(input);

            Assert.False(result);
            return result;
        }
    }
}
