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

    public class HeartNotesServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<HeartNote>>();
            repository.Setup(r => r.All()).Returns(new List<HeartNote>
                                                        {
                                                            new HeartNote(),
                                                            new HeartNote(),
                                                            new HeartNote(),
                                                        }.AsQueryable());
            var service = new HeartNotesService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "HeartNotesTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.HeartNotes.Add(new HeartNote());
            dbContext.HeartNotes.Add(new HeartNote());
            dbContext.HeartNotes.Add(new HeartNote());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<HeartNote>(dbContext);
            var service = new HeartNotesService(repository);
            Assert.Equal(3, service.GetCount());
        }
    }
}
