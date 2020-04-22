namespace MyPerfume.Services.Data.Tests
{
    using System;
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
    using Xunit;

    public class PerfumesServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var designerRepository = new Mock<IDeletableEntityRepository<Designer>>();
            var designerService = new DesignersService(designerRepository.Object);

            var countryRepository = new Mock<IDeletableEntityRepository<Country>>();
            var countriesService = new CountriesService(countryRepository.Object);

            var colorRepository = new Mock<IDeletableEntityRepository<Color>>();
            var colorsService = new ColorsService(colorRepository.Object);

            var pictureUrlRepository = new Mock<IDeletableEntityRepository<PictureUrl>>();
            var pictureUrlService = new PictureUrlsService(pictureUrlRepository.Object);

            var repository = new Mock<IDeletableEntityRepository<Perfume>>();
            repository.Setup(r => r.All()).Returns(new List<Perfume>
                                                        {
                                                            new Perfume(),
                                                            new Perfume(),
                                                            new Perfume(),
                                                        }.AsQueryable());
            var service = new PerfumesService(repository.Object, designerService, countriesService, colorsService, pictureUrlService);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var designerRepository = new Mock<IDeletableEntityRepository<Designer>>();
            var designerService = new DesignersService(designerRepository.Object);

            var countryRepository = new Mock<IDeletableEntityRepository<Country>>();
            var countriesService = new CountriesService(countryRepository.Object);

            var colorRepository = new Mock<IDeletableEntityRepository<Color>>();
            var colorsService = new ColorsService(colorRepository.Object);

            var pictureUrlRepository = new Mock<IDeletableEntityRepository<PictureUrl>>();
            var pictureUrlService = new PictureUrlsService(pictureUrlRepository.Object);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PerfumesTest1Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumes.Add(new Perfume());
            dbContext.Perfumes.Add(new Perfume());
            dbContext.Perfumes.Add(new Perfume());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfume>(dbContext);
            var service = new PerfumesService(repository, designerService, countriesService, colorsService, pictureUrlService);
            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var designerRepository = new Mock<IDeletableEntityRepository<Designer>>();
            var designerService = new DesignersService(designerRepository.Object);

            var countryRepository = new Mock<IDeletableEntityRepository<Country>>();
            var countriesService = new CountriesService(countryRepository.Object);

            var colorRepository = new Mock<IDeletableEntityRepository<Color>>();
            var colorsService = new ColorsService(colorRepository.Object);

            var pictureUrlRepository = new Mock<IDeletableEntityRepository<PictureUrl>>();
            var pictureUrlService = new PictureUrlsService(pictureUrlRepository.Object);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PerfumesTest2Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumes.Add(new Perfume { Id = "A", });
            dbContext.Perfumes.Add(new Perfume { Id = "B", });
            dbContext.Perfumes.Add(new Perfume { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfume>(dbContext);
            var service = new PerfumesService(repository, designerService, countriesService, colorsService, pictureUrlService);
            var result = service.ExistsById("A");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var designerRepository = new Mock<IDeletableEntityRepository<Designer>>();
            var designerService = new DesignersService(designerRepository.Object);

            var countryRepository = new Mock<IDeletableEntityRepository<Country>>();
            var countriesService = new CountriesService(countryRepository.Object);

            var colorRepository = new Mock<IDeletableEntityRepository<Color>>();
            var colorsService = new ColorsService(colorRepository.Object);

            var pictureUrlRepository = new Mock<IDeletableEntityRepository<PictureUrl>>();
            var pictureUrlService = new PictureUrlsService(pictureUrlRepository.Object);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PerfumesTest3Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumes.Add(new Perfume { Id = "A", });
            dbContext.Perfumes.Add(new Perfume { Id = "B", });
            dbContext.Perfumes.Add(new Perfume { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfume>(dbContext);
            var service = new PerfumesService(repository, designerService, countriesService, colorsService, pictureUrlService);
            var result = service.ExistsById("D");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var designerRepository = new Mock<IDeletableEntityRepository<Designer>>();
            var designerService = new DesignersService(designerRepository.Object);

            var countryRepository = new Mock<IDeletableEntityRepository<Country>>();
            var countriesService = new CountriesService(countryRepository.Object);

            var colorRepository = new Mock<IDeletableEntityRepository<Color>>();
            var colorsService = new ColorsService(colorRepository.Object);

            var pictureUrlRepository = new Mock<IDeletableEntityRepository<PictureUrl>>();
            var pictureUrlService = new PictureUrlsService(pictureUrlRepository.Object);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PerfumesTest4Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumes.Add(new Perfume { Id = "A", Name = "E" });
            dbContext.Perfumes.Add(new Perfume { Id = "B", Name = "F" });
            dbContext.Perfumes.Add(new Perfume { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfume>(dbContext);
            var service = new PerfumesService(repository, designerService, countriesService, colorsService, pictureUrlService);
            var result = service.ExistsByName("E");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var designerRepository = new Mock<IDeletableEntityRepository<Designer>>();
            var designerService = new DesignersService(designerRepository.Object);

            var countryRepository = new Mock<IDeletableEntityRepository<Country>>();
            var countriesService = new CountriesService(countryRepository.Object);

            var colorRepository = new Mock<IDeletableEntityRepository<Color>>();
            var colorsService = new ColorsService(colorRepository.Object);

            var pictureUrlRepository = new Mock<IDeletableEntityRepository<PictureUrl>>();
            var pictureUrlService = new PictureUrlsService(pictureUrlRepository.Object);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PerfumesTest5Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumes.Add(new Perfume { Id = "A", Name = "E" });
            dbContext.Perfumes.Add(new Perfume { Id = "B", Name = "F" });
            dbContext.Perfumes.Add(new Perfume { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfume>(dbContext);
            var service = new PerfumesService(repository, designerService, countriesService, colorsService, pictureUrlService);
            var result = service.ExistsByName("H");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<int> EditAsyncShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var designerRepository = new Mock<IDeletableEntityRepository<Designer>>();
            var designerService = new DesignersService(designerRepository.Object);

            var countryRepository = new Mock<IDeletableEntityRepository<Country>>();
            var countriesService = new CountriesService(countryRepository.Object);

            var colorRepository = new Mock<IDeletableEntityRepository<Color>>();
            var colorsService = new ColorsService(colorRepository.Object);

            var pictureUrlRepository = new Mock<IDeletableEntityRepository<PictureUrl>>();
            var pictureUrlService = new PictureUrlsService(pictureUrlRepository.Object);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PerfumesTest6Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumes.Add(new Perfume { Id = "A", Name = "E" });
            dbContext.Perfumes.Add(new Perfume { Id = "B", Name = "F" });
            dbContext.Perfumes.Add(new Perfume { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfume>(dbContext);
            var service = new PerfumesService(repository, designerService, countriesService, colorsService, pictureUrlService);
            var result = await service.EditAsync(new PerfumeDto
            {
                Id = "A",
                Name = "D",
            });

            Assert.Equal(1, result);
            return result;
        }

        [Fact]
        public async Task<int> EditAsyncShouldReturnFalseWithIncorrectInputUsingDbContext()
        {
            var designerRepository = new Mock<IDeletableEntityRepository<Designer>>();
            var designerService = new DesignersService(designerRepository.Object);

            var countryRepository = new Mock<IDeletableEntityRepository<Country>>();
            var countriesService = new CountriesService(countryRepository.Object);

            var colorRepository = new Mock<IDeletableEntityRepository<Color>>();
            var colorsService = new ColorsService(colorRepository.Object);

            var pictureUrlRepository = new Mock<IDeletableEntityRepository<PictureUrl>>();
            var pictureUrlService = new PictureUrlsService(pictureUrlRepository.Object);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PerfumesTest7Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumes.Add(new Perfume { Id = "A", Name = "E" });
            dbContext.Perfumes.Add(new Perfume { Id = "B", Name = "F" });
            dbContext.Perfumes.Add(new Perfume { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfume>(dbContext);
            var service = new PerfumesService(repository, designerService, countriesService, colorsService, pictureUrlService);
            var result = await service.EditAsync(new PerfumeDto
            {
                Id = "H",
                Name = "D",
            });

            Assert.Equal(0, result);
            return result;
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnTrueWithCorrectInputIdUsingDbContext()
        {
            var designerRepository = new Mock<IDeletableEntityRepository<Designer>>();
            var designerService = new DesignersService(designerRepository.Object);

            var countryRepository = new Mock<IDeletableEntityRepository<Country>>();
            var countriesService = new CountriesService(countryRepository.Object);

            var colorRepository = new Mock<IDeletableEntityRepository<Color>>();
            var colorsService = new ColorsService(colorRepository.Object);

            var pictureUrlRepository = new Mock<IDeletableEntityRepository<PictureUrl>>();
            var pictureUrlService = new PictureUrlsService(pictureUrlRepository.Object);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PerfumersTest9Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumes.Add(new Perfume { Id = "A", });
            dbContext.Perfumes.Add(new Perfume { Id = "B", });
            dbContext.Perfumes.Add(new Perfume { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfume>(dbContext);
            var service = new PerfumesService(repository, designerService, countriesService, colorsService, pictureUrlService);
            var result = await service.DeleteAsync("A");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalseWithIncorrectInputIdUsingDbContext()
        {
            var designerRepository = new Mock<IDeletableEntityRepository<Designer>>();
            var designerService = new DesignersService(designerRepository.Object);

            var countryRepository = new Mock<IDeletableEntityRepository<Country>>();
            var countriesService = new CountriesService(countryRepository.Object);

            var colorRepository = new Mock<IDeletableEntityRepository<Color>>();
            var colorsService = new ColorsService(colorRepository.Object);

            var pictureUrlRepository = new Mock<IDeletableEntityRepository<PictureUrl>>();
            var pictureUrlService = new PictureUrlsService(pictureUrlRepository.Object);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PerfumersTest10Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumes.Add(new Perfume { Id = "A", });
            dbContext.Perfumes.Add(new Perfume { Id = "B", });
            dbContext.Perfumes.Add(new Perfume { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfume>(dbContext);
            var service = new PerfumesService(repository, designerService, countriesService, colorsService, pictureUrlService);
            var result = await service.DeleteAsync("D");

            Assert.Equal(0, result);
        }
    }
}
