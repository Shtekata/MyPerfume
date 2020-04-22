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

    public class CategoriesServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Category>>();
            repository.Setup(r => r.All()).Returns(new List<Category>
                                                        {
                                                            new Category(),
                                                            new Category(),
                                                            new Category(),
                                                        }.AsQueryable());
            var service = new CategoriesService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CategoriesTest1Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category());
            dbContext.Categories.Add(new Category());
            dbContext.Categories.Add(new Category());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public async Task AddAsyncShouldReturnCorrectAnswerUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CategoriesTest2Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category());
            dbContext.Categories.Add(new Category());
            dbContext.Categories.Add(new Category());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            var result = await service.AddAsync(new BaseDto());

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "CategoriesTest3Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category { Id = "A", });
            dbContext.Categories.Add(new Category { Id = "B", });
            dbContext.Categories.Add(new Category { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            var result = service.ExistsById("A");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByIdShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "CategoriesTest4Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category { Id = "A", });
            dbContext.Categories.Add(new Category { Id = "B", });
            dbContext.Categories.Add(new Category { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            var result = service.ExistsById("D");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "CategoriesTest5Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category { Id = "A", Name = "E" });
            dbContext.Categories.Add(new Category { Id = "B", Name = "F" });
            dbContext.Categories.Add(new Category { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            var result = service.ExistsByName("E");

            Assert.True(result);
            return result;
        }

        [Fact]
        public async Task<bool> ExistsByNameShouldReturnFalseWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "CategoriesTest6Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category { Id = "A", Name = "E" });
            dbContext.Categories.Add(new Category { Id = "B", Name = "F" });
            dbContext.Categories.Add(new Category { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            var result = service.ExistsByName("H");

            Assert.False(result);
            return result;
        }

        [Fact]
        public async Task<int> EditAsyncShouldReturnTrueWithCorrectInputUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "CategoriesTest7Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category { Id = "A", Name = "E" });
            dbContext.Categories.Add(new Category { Id = "B", Name = "F" });
            dbContext.Categories.Add(new Category { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
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
               .UseInMemoryDatabase(databaseName: "CategoriesTest8Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category { Id = "A", Name = "E" });
            dbContext.Categories.Add(new Category { Id = "B", Name = "F" });
            dbContext.Categories.Add(new Category { Id = "C", Name = "G" });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
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
            dbContext.Categories.Add(new Category { Id = "A", });
            dbContext.Categories.Add(new Category { Id = "B", });
            dbContext.Categories.Add(new Category { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            var result = await service.DeleteAsync("A");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalseWithIncorrectInputIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CategoriesTest10Db").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category { Id = "A", });
            dbContext.Categories.Add(new Category { Id = "B", });
            dbContext.Categories.Add(new Category { Id = "C", });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            var result = await service.DeleteAsync("D");

            Assert.Equal(0, result);
        }
    }
}
