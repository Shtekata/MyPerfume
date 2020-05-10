namespace MyPerfume.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using MyPerfume.Data.Models;

    public class ColorsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            if (dbContext.Colors.Any())
            {
                return;
            }

            var colors = new List<string>() { "Безцветен", "Светъл и бистър", "По-тъмен и наситен цвят" };

            foreach (var color in colors)
            {
                await dbContext.Colors.AddAsync(new Color { Name = color });
            }
        }
    }
}
