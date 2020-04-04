namespace MyPerfume.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyPerfume.Data.Models;

    public class TopNotesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Designers.Any())
            {
                return;
            }

            var topNotes = new List<string>() { "Chanel", "Алдехиди" };

            foreach (var topNote in topNotes)
            {
                await dbContext.TopNotes.AddAsync(new TopNote { Name = topNote });
            }
        }
    }
}
