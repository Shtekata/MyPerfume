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

    public class BaseNotesServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<BaseNote>>();
            repository.Setup(r => r.All()).Returns(new List<BaseNote>
                                                        {
                                                            new BaseNote(),
                                                            new BaseNote(),
                                                            new BaseNote(),
                                                        }.AsQueryable());
            var service = new BaseNotesService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BaseNotesTest1Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.BaseNotes.Add(new BaseNote());
            dbContext.BaseNotes.Add(new BaseNote());
            dbContext.BaseNotes.Add(new BaseNote());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<BaseNote>(dbContext);
            var service = new BaseNotesService(repository);
            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public async Task AddAsyncShouldReturnCorrectAnswerUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BaseNotesTest2Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.BaseNotes.Add(new BaseNote());
            dbContext.BaseNotes.Add(new BaseNote());
            dbContext.BaseNotes.Add(new BaseNote());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<BaseNote>(dbContext);
            var service = new BaseNotesService(repository);
            var result = await service.AddAsync(new BaseDto());

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "BaseNotesTest3Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.BaseNotes.Add(new BaseNote { Id = "A", });
            dbContext.BaseNotes.Add(new BaseNote { Id = "B", });
            dbContext.BaseNotes.Add(new BaseNote { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<BaseNote>(dbContext);
            var service = new BaseNotesService(repository);
            var result = service.ExistsById("A");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "BaseNotesTest4Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.BaseNotes.Add(new BaseNote { Id = "A", });
            dbContext.BaseNotes.Add(new BaseNote { Id = "B", });
            dbContext.BaseNotes.Add(new BaseNote { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<BaseNote>(dbContext);
            var service = new BaseNotesService(repository);
            var result = service.ExistsById("D");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "BaseNotesTest5Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.BaseNotes.Add(new BaseNote { Id = "A", Name = "E" });
            dbContext.BaseNotes.Add(new BaseNote { Id = "B", Name = "F" });
            dbContext.BaseNotes.Add(new BaseNote { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<BaseNote>(dbContext);
            var service = new BaseNotesService(repository);
            var result = service.ExistsByName("E");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "BaseNotesTest6Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.BaseNotes.Add(new BaseNote { Id = "A", Name = "E" });
            dbContext.BaseNotes.Add(new BaseNote { Id = "B", Name = "F" });
            dbContext.BaseNotes.Add(new BaseNote { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<BaseNote>(dbContext);
            var service = new BaseNotesService(repository);
            var result = service.ExistsByName("H");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<int> EditAsyncShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "BaseNotesTest7Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.BaseNotes.Add(new BaseNote { Id = "A", Name = "E" });
            dbContext.BaseNotes.Add(new BaseNote { Id = "B", Name = "F" });
            dbContext.BaseNotes.Add(new BaseNote { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<BaseNote>(dbContext);
            var service = new BaseNotesService(repository);
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
               .UseInMemoryDatabase(databaseName: "BaseNotesTest8Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.BaseNotes.Add(new BaseNote { Id = "A", Name = "E" });
            dbContext.BaseNotes.Add(new BaseNote { Id = "B", Name = "F" });
            dbContext.BaseNotes.Add(new BaseNote { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<BaseNote>(dbContext);
            var service = new BaseNotesService(repository);
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
                .UseInMemoryDatabase(databaseName: "BaseNotesTest9Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.BaseNotes.Add(new BaseNote { Id = "A", });
            dbContext.BaseNotes.Add(new BaseNote { Id = "B", });
            dbContext.BaseNotes.Add(new BaseNote { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<BaseNote>(dbContext);
            var service = new BaseNotesService(repository);
            var result = await service.DeleteAsync("A");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalseWithIncorrectInputIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BaseNotesTest10Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.BaseNotes.Add(new BaseNote { Id = "A", });
            dbContext.BaseNotes.Add(new BaseNote { Id = "B", });
            dbContext.BaseNotes.Add(new BaseNote { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<BaseNote>(dbContext);
            var service = new BaseNotesService(repository);
            var result = await service.DeleteAsync("D");

            Assert.Equal(0, result);
        }
    }
}
