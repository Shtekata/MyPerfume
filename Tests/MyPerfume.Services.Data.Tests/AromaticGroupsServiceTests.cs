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
                .UseInMemoryDatabase(databaseName: "AromaticGroupsTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.AromaticGroups.Add(new AromaticGroup());
            dbContext.AromaticGroups.Add(new AromaticGroup());
            dbContext.AromaticGroups.Add(new AromaticGroup());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<AromaticGroup>(dbContext);
            var service = new AromaticGroupsService(repository);
            Assert.Equal(3, service.GetCount());
        }
    }
}
