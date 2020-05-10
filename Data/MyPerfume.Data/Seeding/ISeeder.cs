namespace MyPerfume.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;

    public interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider, IConfiguration configuration);
    }
}
