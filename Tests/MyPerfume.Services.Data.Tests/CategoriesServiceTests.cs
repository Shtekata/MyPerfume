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
                .UseInMemoryDatabase(databaseName: "CategoriesTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category());
            dbContext.Categories.Add(new Category());
            dbContext.Categories.Add(new Category());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            Assert.Equal(3, service.GetCount());
        }
    }
}
