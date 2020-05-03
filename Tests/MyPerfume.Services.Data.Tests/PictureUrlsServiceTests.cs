namespace MyPerfume.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Moq;
    using MyPerfume.Data;
    using MyPerfume.Data.Common.Repositories;
    using MyPerfume.Data.Models;
    using MyPerfume.Data.Repositories;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.ViewModels;
    using Xunit;

    public class PictureUrlsServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
            var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();

            var repository = new Mock<IDeletableEntityRepository<PictureUrl>>();
            repository.Setup(r => r.All()).Returns(new List<PictureUrl>
                                                        {
                                                            new PictureUrl(),
                                                            new PictureUrl(),
                                                            new PictureUrl(),
                                                        }.AsQueryable());
            var service = new PictureUrlsService(repository.Object, perfumePictureUrlRepository.Object, perfumeRepository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
            var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PictureUrlsTest1Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PictureUrls.Add(new PictureUrl());
            dbContext.PictureUrls.Add(new PictureUrl());
            dbContext.PictureUrls.Add(new PictureUrl());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository, perfumePictureUrlRepository.Object, perfumeRepository.Object);
            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
            var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest2Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PictureUrls.Add(new PictureUrl { Id = "A", });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "B", });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository, perfumePictureUrlRepository.Object, perfumeRepository.Object);
            var result = service.ExistsById("A");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
            var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest3Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PictureUrls.Add(new PictureUrl { Id = "A", });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "B", });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository, perfumePictureUrlRepository.Object, perfumeRepository.Object);
            var result = service.ExistsById("D");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByUrlShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
            var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest4Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PictureUrls.Add(new PictureUrl { Id = "A", Url = "E" });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "B", Url = "F" });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "C", Url = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository, perfumePictureUrlRepository.Object, perfumeRepository.Object);
            var result = service.ExistsByUrl("B", "E");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByUrlShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
            var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest5Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PictureUrls.Add(new PictureUrl { Id = "A", Url = "E" });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "B", Url = "F" });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "C", Url = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository, perfumePictureUrlRepository.Object, perfumeRepository.Object);
            var result = service.ExistsByUrl("B", "F");

            Assert.False(result);
            return result;
        }

        // [Fact]
        // public async Task<int> EditAsyncShouldReturnTrueWithCorrectInputUsingPerfumeDtoAndDbContext()
        // {
        //    var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
        //    await perfumeRepository.Object.AddAsync(new Perfume
        //    {
        //        Id = "PerfumeId1",
        //    });
        //    await perfumeRepository.Object.SaveChangesAsync();

        // var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();
        //    await perfumePictureUrlRepository.Object.AddAsync(new PerfumePictureUrl
        //    {
        //        PerfumeId = "PerfumeId1",
        //        PictureUrlId = "PictureUrlId1",
        //    });
        //    await perfumePictureUrlRepository.Object.SaveChangesAsync();

        // var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //       .UseInMemoryDatabase(databaseName: "PictureUrlsTest6Db").Options;
        //    var dbContext = new ApplicationDbContext(options);
        //    dbContext.PictureUrls.Add(new PictureUrl { Id = "PerfumeId1" });
        //    dbContext.PictureUrls.Add(new PictureUrl { Id = "PerfumeId2" });
        //    dbContext.PictureUrls.Add(new PictureUrl { Id = "PerfumeId3" });
        //    await dbContext.SaveChangesAsync();

        // var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
        //    var service = new PictureUrlsService(repository, perfumePictureUrlRepository.Object, perfumeRepository.Object);
        //    var result = await service.EditAsync(new PerfumeDto
        //    {
        //        Id = "PerfumeId1",
        //        Extensions = new Dictionary<string, List<SelectListItem>>()
        //        {
        //            {
        //                "PictureUrls", new List<SelectListItem>
        //            {
        //                new SelectListItem
        //                {
        //                    Value = "PictureUrlId1",
        //                    Selected = false,
        //                },
        //            }
        //            },
        //        },
        //    });

        // Assert.Equal(1, result);
        //    return result;
        // }

        // [Fact]
        // public async Task<int> EditAsyncShouldReturnFalseWithIncorrectInputUsingPerfumeDtoAndDbContext()
        // {
        //    var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
        //    var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();
        //    await perfumePictureUrlRepository.Object.AddAsync(new PerfumePictureUrl
        //    {
        //        PerfumeId = "PerfumeId1",
        //        PictureUrlId = "PictureUrlId1",
        //    });
        //    await perfumePictureUrlRepository.Object.SaveChangesAsync();

        // var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //       .UseInMemoryDatabase(databaseName: "PictureUrlsTest6Db").Options;
        //    var dbContext = new ApplicationDbContext(options);
        //    dbContext.PictureUrls.Add(new PictureUrl { Id = "PerfumeId1" });
        //    dbContext.PictureUrls.Add(new PictureUrl { Id = "PerfumeId2" });
        //    dbContext.PictureUrls.Add(new PictureUrl { Id = "PerfumeId3" });
        //    await dbContext.SaveChangesAsync();

        // var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
        //    var service = new PictureUrlsService(repository, perfumePictureUrlRepository.Object, perfumeRepository.Object);
        //    var result = await service.EditAsync(new PerfumeDto
        //    {
        //        Id = "PerfumeId1",
        //        Extensions = new Dictionary<string, List<SelectListItem>>()
        //        {
        //            {
        //                "PictureUrls", new List<SelectListItem>
        //            {
        //                new SelectListItem
        //                {
        //                    Value = "PictureUrlId1",
        //                    Selected = false,
        //                },
        //            }
        //            },
        //        },
        //    });

        // Assert.Equal(0, result);
        //    return result;
        // }
        [Fact]
        public async Task<int> EditAsyncShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
            var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest8Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PictureUrls.Add(new PictureUrl { Id = "A", PictureShowNumber = 1 });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "B", PictureShowNumber = 2 });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "C", PictureShowNumber = 3 });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository, perfumePictureUrlRepository.Object, perfumeRepository.Object);
            var result = await service.EditAsync(new PictureUrlDto
            {
                Id = "A",
                PictureShowNumber = 5,
            });

            Assert.Equal(1, result);
            return result;
        }

        [Fact]
        public async Task<int> EditAsyncShouldReturnFalseWithIncorrectInputUsingDbContext()
        {
            var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
            var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest9Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PictureUrls.Add(new PictureUrl { Id = "A", PictureShowNumber = 1 });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "B", PictureShowNumber = 2 });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "C", PictureShowNumber = 3 });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository, perfumePictureUrlRepository.Object, perfumeRepository.Object);
            var result = await service.EditAsync(new PictureUrlDto
            {
                Id = "D",
                PictureShowNumber = 5,
            });

            Assert.Equal(0, result);
            return result;
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnTrueWithCorrectInputIdUsingDbContext()
        {
            var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
            var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PictureUrlsTest10Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PictureUrls.Add(new PictureUrl { Id = "A", });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "B", });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository, perfumePictureUrlRepository.Object, perfumeRepository.Object);
            var result = await service.DeleteAsync("A");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalseWithIncorrectInputIdUsingDbContext()
        {
            var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
            var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PictureUrlsTest11Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PictureUrls.Add(new PictureUrl { Id = "A", });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "B", });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository, perfumePictureUrlRepository.Object, perfumeRepository.Object);
            var result = await service.DeleteAsync("D");

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task<bool> IsTheSameInputShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
            var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest12Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PictureUrls.Add(new PictureUrl { Id = "A", DesignerName = "E" });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "B", DesignerName = "F" });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "C", DesignerName = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository, perfumePictureUrlRepository.Object, perfumeRepository.Object);
            var input = new PictureUrlDto
            {
                Id = "A",
                DesignerName = "E",
            };
            var result = service.IsTheSameInput(input);

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> IsTheSameInputShouldReturnFalseWithIncorrectInputUsingDbContext()
        {
            var perfumeRepository = new Mock<IDeletableEntityRepository<Perfume>>();
            var perfumePictureUrlRepository = new Mock<IDeletableEntityRepository<PerfumePictureUrl>>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PictureUrlsTest13Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.PictureUrls.Add(new PictureUrl { Id = "A", DesignerName = "E" });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "B", DesignerName = "F" });
            dbContext.PictureUrls.Add(new PictureUrl { Id = "C", DesignerName = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<PictureUrl>(dbContext);
            var service = new PictureUrlsService(repository, perfumePictureUrlRepository.Object, perfumeRepository.Object);
            var input = new PictureUrlDto
            {
                Id = "A",
                DesignerName = "H",
            };
            var result = service.IsTheSameInput(input);

            Assert.False(result);
            return result;
        }
    }
}
