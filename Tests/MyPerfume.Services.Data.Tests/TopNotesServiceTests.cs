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
                .UseInMemoryDatabase(databaseName: "TopNotesTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.TopNotes.Add(new TopNote());
            dbContext.TopNotes.Add(new TopNote());
            dbContext.TopNotes.Add(new TopNote());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<TopNote>(dbContext);
            var service = new TopNotesService(repository);
            Assert.Equal(3, service.GetCount());
        }
    }
}
