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
                .UseInMemoryDatabase(databaseName: "CountriesTes1tDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Countries.Add(new Country());
            dbContext.Countries.Add(new Country());
            dbContext.Countries.Add(new Country());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Country>(dbContext);
            var service = new CountriesService(repository);
            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public async Task AddAsyncShouldReturnCorrectAnswerUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CountriesTes2tDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Countries.Add(new Country());
            dbContext.Countries.Add(new Country());
            dbContext.Countries.Add(new Country());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Country>(dbContext);
            var service = new CountriesService(repository);
            var result = await service.AddAsync(new BaseDto());

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "CountriesTes3tDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Countries.Add(new Country { Id = "A", });
            dbContext.Countries.Add(new Country { Id = "B", });
            dbContext.Countries.Add(new Country { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Country>(dbContext);
            var service = new CountriesService(repository);
            var result = service.ExistsById("A");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "CountriesTes4tDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Countries.Add(new Country { Id = "A", });
            dbContext.Countries.Add(new Country { Id = "B", });
            dbContext.Countries.Add(new Country { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Country>(dbContext);
            var service = new CountriesService(repository);
            var result = service.ExistsById("D");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "CountriesTes5tDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Countries.Add(new Country { Id = "A", Name = "E" });
            dbContext.Countries.Add(new Country { Id = "B", Name = "F" });
            dbContext.Countries.Add(new Country { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Country>(dbContext);
            var service = new CountriesService(repository);
            var result = service.ExistsByName("E");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "CountriesTes6tDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Countries.Add(new Country { Id = "A", Name = "E" });
            dbContext.Countries.Add(new Country { Id = "B", Name = "F" });
            dbContext.Countries.Add(new Country { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Country>(dbContext);
            var service = new CountriesService(repository);
            var result = service.ExistsByName("H");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<int> EditAsyncShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "CountriesTes7tDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Countries.Add(new Country { Id = "A", Name = "E" });
            dbContext.Countries.Add(new Country { Id = "B", Name = "F" });
            dbContext.Countries.Add(new Country { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Country>(dbContext);
            var service = new CountriesService(repository);
            var result = await service.EditAsync(new BaseDto
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
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "CountriesTes8tDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Countries.Add(new Country { Id = "A", Name = "E" });
            dbContext.Countries.Add(new Country { Id = "B", Name = "F" });
            dbContext.Countries.Add(new Country { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Country>(dbContext);
            var service = new CountriesService(repository);
            var result = await service.EditAsync(new BaseDto
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
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CategoriesTest9Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Countries.Add(new Country { Id = "A", });
            dbContext.Countries.Add(new Country { Id = "B", });
            dbContext.Countries.Add(new Country { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Country>(dbContext);
            var service = new CountriesService(repository);
            var result = await service.DeleteAsync("A");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalseWithIncorrectInputIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CategoriesTest10Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Countries.Add(new Country { Id = "A", });
            dbContext.Countries.Add(new Country { Id = "B", });
            dbContext.Countries.Add(new Country { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Country>(dbContext);
            var service = new CountriesService(repository);
            var result = await service.DeleteAsync("D");

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task<bool> IsTheSameInputShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "CategoriesTest11Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Countries.Add(new Country { Id = "A", Name = "E" });
            dbContext.Countries.Add(new Country { Id = "B", Name = "F" });
            dbContext.Countries.Add(new Country { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Country>(dbContext);
            var service = new CountriesService(repository);
            var input = new BaseDto
            {
                Id = "A",
                Name = "E",
            };
            var result = service.IsTheSameInput(input);

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> IsTheSameInputShouldReturnFalseWithIncorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "CategoriesTest12Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Countries.Add(new Country { Id = "A", Name = "E" });
            dbContext.Countries.Add(new Country { Id = "B", Name = "F" });
            dbContext.Countries.Add(new Country { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Country>(dbContext);
            var service = new CountriesService(repository);
            var input = new BaseDto
            {
                Id = "A",
                Name = "H",
            };
            var result = service.IsTheSameInput(input);

            Assert.False(result);
            return result;
        }
    }
}
