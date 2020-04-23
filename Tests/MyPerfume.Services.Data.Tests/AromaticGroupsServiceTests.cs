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

    public class AromaticGroupsServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<AromaticGroup>>();
            repository.Setup(r => r.All()).Returns(new List<AromaticGroup>
                                                        {
                                                            new AromaticGroup(),
                                                            new AromaticGroup(),
                                                            new AromaticGroup(),
                                                        }.AsQueryable());
            var service = new AromaticGroupsService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AromaticGroupsTest1Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.AromaticGroups.Add(new AromaticGroup());
            dbContext.AromaticGroups.Add(new AromaticGroup());
            dbContext.AromaticGroups.Add(new AromaticGroup());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<AromaticGroup>(dbContext);
            var service = new AromaticGroupsService(repository);
            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public async Task AddAsyncShouldReturnCorrectAnswerUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AromaticGroupsTest2Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.AromaticGroups.Add(new AromaticGroup());
            dbContext.AromaticGroups.Add(new AromaticGroup());
            dbContext.AromaticGroups.Add(new AromaticGroup());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<AromaticGroup>(dbContext);
            var service = new AromaticGroupsService(repository);
            var result = await service.AddAsync(new BaseDto());

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "AromaticGroupsTest3Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "A", });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "B", });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<AromaticGroup>(dbContext);
            var service = new AromaticGroupsService(repository);
            var result = service.ExistsById("A");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "AromaticGroupsTest4Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "A", });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "B", });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<AromaticGroup>(dbContext);
            var service = new AromaticGroupsService(repository);
            var result = service.ExistsById("D");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "AromaticGroupsTest5Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "A", Name = "E" });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "B", Name = "F" });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<AromaticGroup>(dbContext);
            var service = new AromaticGroupsService(repository);
            var result = service.ExistsByName("E");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "AromaticGroupsTest6Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "A", Name = "E" });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "B", Name = "F" });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<AromaticGroup>(dbContext);
            var service = new AromaticGroupsService(repository);
            var result = service.ExistsByName("H");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<int> EditAsyncShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "AromaticGroupsTest7Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "A", Name = "E" });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "B", Name = "F" });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<AromaticGroup>(dbContext);
            var service = new AromaticGroupsService(repository);
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
               .UseInMemoryDatabase(databaseName: "AromaticGroupsTest8Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "A", Name = "E" });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "B", Name = "F" });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<AromaticGroup>(dbContext);
            var service = new AromaticGroupsService(repository);
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
                .UseInMemoryDatabase(databaseName: "AromaticGroupsTest9Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "A", });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "B", });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<AromaticGroup>(dbContext);
            var service = new AromaticGroupsService(repository);
            var result = await service.DeleteAsync("A");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalseWithIncorrectInputIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AromaticGroupsTest10Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "A", });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "B", });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<AromaticGroup>(dbContext);
            var service = new AromaticGroupsService(repository);
            var result = await service.DeleteAsync("D");

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task<bool> IsTheSameInputShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "AromaticGroupsTest11Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "A", Name = "E" });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "B", Name = "F" });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<AromaticGroup>(dbContext);
            var service = new AromaticGroupsService(repository);
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
               .UseInMemoryDatabase(databaseName: "AromaticGroupsTest12Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "A", Name = "E" });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "B", Name = "F" });
            dbContext.AromaticGroups.Add(new AromaticGroup { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<AromaticGroup>(dbContext);
            var service = new AromaticGroupsService(repository);
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
