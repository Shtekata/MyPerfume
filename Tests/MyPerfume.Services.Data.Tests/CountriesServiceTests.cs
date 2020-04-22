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

    public class CountriesServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Country>>();
            repository.Setup(r => r.All()).Returns(new List<Country>
                                                        {
                                                            new Country(),
                                                            new Country(),
                                                            new Country(),
                                                        }.AsQueryable());
            var service = new CountriesService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CountriesTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Countries.Add(new Country());
            dbContext.Countries.Add(new Country());
            dbContext.Countries.Add(new Country());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Country>(dbContext);
            var service = new CountriesService(repository);
            Assert.Equal(3, service.GetCount());
        }
    }
}
