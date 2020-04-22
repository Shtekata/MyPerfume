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
                .UseInMemoryDatabase(databaseName: "BaseNotesTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.BaseNotes.Add(new BaseNote());
            dbContext.BaseNotes.Add(new BaseNote());
            dbContext.BaseNotes.Add(new BaseNote());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<BaseNote>(dbContext);
            var service = new BaseNotesService(repository);
            Assert.Equal(3, service.GetCount());
        }
    }
}
