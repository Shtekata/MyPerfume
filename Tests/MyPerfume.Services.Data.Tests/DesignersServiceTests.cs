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

    public class DesignersServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Designer>>();
            repository.Setup(r => r.All()).Returns(new List<Designer>
                                                        {
                                                            new Designer(),
                                                            new Designer(),
                                                            new Designer(),
                                                        }.AsQueryable());
            var service = new DesignersService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DesignersTest1Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Designers.Add(new Designer());
            dbContext.Designers.Add(new Designer());
            dbContext.Designers.Add(new Designer());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Designer>(dbContext);
            var service = new DesignersService(repository);
            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public async Task AddAsyncShouldReturnCorrectAnswerUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DesignersTest2Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Designers.Add(new Designer());
            dbContext.Designers.Add(new Designer());
            dbContext.Designers.Add(new Designer());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Designer>(dbContext);
            var service = new DesignersService(repository);
            var result = await service.AddAsync(new BaseDto());

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "DesignersTest3Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Designers.Add(new Designer { Id = "A", });
            dbContext.Designers.Add(new Designer { Id = "B", });
            dbContext.Designers.Add(new Designer { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Designer>(dbContext);
            var service = new DesignersService(repository);
            var result = service.ExistsById("A");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "DesignersTest4Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Designers.Add(new Designer { Id = "A", });
            dbContext.Designers.Add(new Designer { Id = "B", });
            dbContext.Designers.Add(new Designer { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Designer>(dbContext);
            var service = new DesignersService(repository);
            var result = service.ExistsById("D");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "DesignersTest5Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Designers.Add(new Designer { Id = "A", Name = "E" });
            dbContext.Designers.Add(new Designer { Id = "B", Name = "F" });
            dbContext.Designers.Add(new Designer { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Designer>(dbContext);
            var service = new DesignersService(repository);
            var result = service.ExistsByName("E");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "DesignersTest6Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Designers.Add(new Designer { Id = "A", Name = "E" });
            dbContext.Designers.Add(new Designer { Id = "B", Name = "F" });
            dbContext.Designers.Add(new Designer { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Designer>(dbContext);
            var service = new DesignersService(repository);
            var result = service.ExistsByName("H");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<int> EditAsyncShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "DesignersTest7Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Designers.Add(new Designer { Id = "A", Name = "E" });
            dbContext.Designers.Add(new Designer { Id = "B", Name = "F" });
            dbContext.Designers.Add(new Designer { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Designer>(dbContext);
            var service = new DesignersService(repository);
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
               .UseInMemoryDatabase(databaseName: "DesignersTest8Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Designers.Add(new Designer { Id = "A", Name = "E" });
            dbContext.Designers.Add(new Designer { Id = "B", Name = "F" });
            dbContext.Designers.Add(new Designer { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Designer>(dbContext);
            var service = new DesignersService(repository);
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
            dbContext.Designers.Add(new Designer { Id = "A", });
            dbContext.Designers.Add(new Designer { Id = "B", });
            dbContext.Designers.Add(new Designer { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Designer>(dbContext);
            var service = new DesignersService(repository);
            var result = await service.DeleteAsync("A");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalseWithIncorrectInputIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CategoriesTest10Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Designers.Add(new Designer { Id = "A", });
            dbContext.Designers.Add(new Designer { Id = "B", });
            dbContext.Designers.Add(new Designer { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Designer>(dbContext);
            var service = new DesignersService(repository);
            var result = await service.DeleteAsync("D");

            Assert.Equal(0, result);
        }
    }
}
