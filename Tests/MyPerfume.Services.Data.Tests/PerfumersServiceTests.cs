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

    public class PerfumersServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Perfumer>>();
            repository.Setup(r => r.All()).Returns(new List<Perfumer>
                                                        {
                                                            new Perfumer(),
                                                            new Perfumer(),
                                                            new Perfumer(),
                                                        }.AsQueryable());
            var service = new PerfumersService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PerfumersTest1Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumers.Add(new Perfumer());
            dbContext.Perfumers.Add(new Perfumer());
            dbContext.Perfumers.Add(new Perfumer());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfumer>(dbContext);
            var service = new PerfumersService(repository);
            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public async Task AddAsyncShouldReturnCorrectAnswerUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PerfumersTest2Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumers.Add(new Perfumer());
            dbContext.Perfumers.Add(new Perfumer());
            dbContext.Perfumers.Add(new Perfumer());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfumer>(dbContext);
            var service = new PerfumersService(repository);
            var result = await service.AddAsync(new BaseDto());

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PerfumersTest3Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumers.Add(new Perfumer { Id = "A", });
            dbContext.Perfumers.Add(new Perfumer { Id = "B", });
            dbContext.Perfumers.Add(new Perfumer { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfumer>(dbContext);
            var service = new PerfumersService(repository);
            var result = service.ExistsById("A");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PerfumersTest4Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumers.Add(new Perfumer { Id = "A", });
            dbContext.Perfumers.Add(new Perfumer { Id = "B", });
            dbContext.Perfumers.Add(new Perfumer { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfumer>(dbContext);
            var service = new PerfumersService(repository);
            var result = service.ExistsById("D");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PerfumersTest5Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumers.Add(new Perfumer { Id = "A", Name = "E" });
            dbContext.Perfumers.Add(new Perfumer { Id = "B", Name = "F" });
            dbContext.Perfumers.Add(new Perfumer { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfumer>(dbContext);
            var service = new PerfumersService(repository);
            var result = service.ExistsByName("E");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PerfumersTest6Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumers.Add(new Perfumer { Id = "A", Name = "E" });
            dbContext.Perfumers.Add(new Perfumer { Id = "B", Name = "F" });
            dbContext.Perfumers.Add(new Perfumer { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfumer>(dbContext);
            var service = new PerfumersService(repository);
            var result = service.ExistsByName("H");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<int> EditAsyncShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PerfumersTest7Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumers.Add(new Perfumer { Id = "A", Name = "E" });
            dbContext.Perfumers.Add(new Perfumer { Id = "B", Name = "F" });
            dbContext.Perfumers.Add(new Perfumer { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfumer>(dbContext);
            var service = new PerfumersService(repository);
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
               .UseInMemoryDatabase(databaseName: "PerfumersTest8Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumers.Add(new Perfumer { Id = "A", Name = "E" });
            dbContext.Perfumers.Add(new Perfumer { Id = "B", Name = "F" });
            dbContext.Perfumers.Add(new Perfumer { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfumer>(dbContext);
            var service = new PerfumersService(repository);
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
                .UseInMemoryDatabase(databaseName: "PerfumersTest9Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumers.Add(new Perfumer { Id = "A", });
            dbContext.Perfumers.Add(new Perfumer { Id = "B", });
            dbContext.Perfumers.Add(new Perfumer { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfumer>(dbContext);
            var service = new PerfumersService(repository);
            var result = await service.DeleteAsync("A");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalseWithIncorrectInputIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PerfumersTest10Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumers.Add(new Perfumer { Id = "A", });
            dbContext.Perfumers.Add(new Perfumer { Id = "B", });
            dbContext.Perfumers.Add(new Perfumer { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfumer>(dbContext);
            var service = new PerfumersService(repository);
            var result = await service.DeleteAsync("D");

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task<bool> IsTheSameInputShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PerfumersTest11Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumers.Add(new Perfumer { Id = "A", Name = "E" });
            dbContext.Perfumers.Add(new Perfumer { Id = "B", Name = "F" });
            dbContext.Perfumers.Add(new Perfumer { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfumer>(dbContext);
            var service = new PerfumersService(repository);
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
               .UseInMemoryDatabase(databaseName: "PerfumersTest12Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Perfumers.Add(new Perfumer { Id = "A", Name = "E" });
            dbContext.Perfumers.Add(new Perfumer { Id = "B", Name = "F" });
            dbContext.Perfumers.Add(new Perfumer { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Perfumer>(dbContext);
            var service = new PerfumersService(repository);
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
