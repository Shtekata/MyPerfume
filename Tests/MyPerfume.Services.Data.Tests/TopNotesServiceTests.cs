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

    public class TopNotesServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<TopNote>>();
            repository.Setup(r => r.All()).Returns(new List<TopNote>
                                                        {
                                                            new TopNote(),
                                                            new TopNote(),
                                                            new TopNote(),
                                                        }.AsQueryable());
            var service = new TopNotesService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TopNotesTest1Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.TopNotes.Add(new TopNote());
            dbContext.TopNotes.Add(new TopNote());
            dbContext.TopNotes.Add(new TopNote());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<TopNote>(dbContext);
            var service = new TopNotesService(repository);
            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public async Task AddAsyncShouldReturnCorrectAnswerUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TopNotesTest2Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.TopNotes.Add(new TopNote());
            dbContext.TopNotes.Add(new TopNote());
            dbContext.TopNotes.Add(new TopNote());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<TopNote>(dbContext);
            var service = new TopNotesService(repository);
            var result = await service.AddAsync(new BaseDto());

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "TopNotesTest3Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.TopNotes.Add(new TopNote { Id = "A", });
            dbContext.TopNotes.Add(new TopNote { Id = "B", });
            dbContext.TopNotes.Add(new TopNote { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<TopNote>(dbContext);
            var service = new TopNotesService(repository);
            var result = service.ExistsById("A");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "TopNotesTest4Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.TopNotes.Add(new TopNote { Id = "A", });
            dbContext.TopNotes.Add(new TopNote { Id = "B", });
            dbContext.TopNotes.Add(new TopNote { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<TopNote>(dbContext);
            var service = new TopNotesService(repository);
            var result = service.ExistsById("D");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "TopNotesTest5Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.TopNotes.Add(new TopNote { Id = "A", Name = "E" });
            dbContext.TopNotes.Add(new TopNote { Id = "B", Name = "F" });
            dbContext.TopNotes.Add(new TopNote { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<TopNote>(dbContext);
            var service = new TopNotesService(repository);
            var result = service.ExistsByName("E");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "TopNotesTest6Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.TopNotes.Add(new TopNote { Id = "A", Name = "E" });
            dbContext.TopNotes.Add(new TopNote { Id = "B", Name = "F" });
            dbContext.TopNotes.Add(new TopNote { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<TopNote>(dbContext);
            var service = new TopNotesService(repository);
            var result = service.ExistsByName("H");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<int> EditAsyncShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "TopNotesTest7Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.TopNotes.Add(new TopNote { Id = "A", Name = "E" });
            dbContext.TopNotes.Add(new TopNote { Id = "B", Name = "F" });
            dbContext.TopNotes.Add(new TopNote { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<TopNote>(dbContext);
            var service = new TopNotesService(repository);
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
               .UseInMemoryDatabase(databaseName: "TopNotesTest8Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.TopNotes.Add(new TopNote { Id = "A", Name = "E" });
            dbContext.TopNotes.Add(new TopNote { Id = "B", Name = "F" });
            dbContext.TopNotes.Add(new TopNote { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<TopNote>(dbContext);
            var service = new TopNotesService(repository);
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
            dbContext.TopNotes.Add(new TopNote { Id = "A", });
            dbContext.TopNotes.Add(new TopNote { Id = "B", });
            dbContext.TopNotes.Add(new TopNote { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<TopNote>(dbContext);
            var service = new TopNotesService(repository);
            var result = await service.DeleteAsync("A");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalseWithIncorrectInputIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CategoriesTest10Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.TopNotes.Add(new TopNote { Id = "A", });
            dbContext.TopNotes.Add(new TopNote { Id = "B", });
            dbContext.TopNotes.Add(new TopNote { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<TopNote>(dbContext);
            var service = new TopNotesService(repository);
            var result = await service.DeleteAsync("D");

            Assert.Equal(0, result);
        }
    }
}
