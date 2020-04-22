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
                .UseInMemoryDatabase(databaseName: "PerfumesTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumes.Add(new Perfume());
            dbContext.Perfumes.Add(new Perfume());
            dbContext.Perfumes.Add(new Perfume());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfume>(dbContext);
            var service = new PerfumesService(repository, designerService, countriesService, colorsService, pictureUrlService);
            Assert.Equal(3, service.GetCount());
        }
    }
}
