namespace MyPerfume.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyPerfume.Data.Models;

    public class DesignersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Designers.Any())
            {
                return;
            }

            var designers = new List<string>() { "Chanel", "Christian Dior", "Thierry Mugler", "Guerlain", "Yves Saint Laurent", "Tom Ford", "Lancôme", "Givenchy", "Giorgio Armani", "Paco Rabanne", "Versace", "Roberto Cavalli", "Carolina Herrera", "Kenzo", "Calvin Klein", "Dolce & Gabbana", "Azzaro", "Narciso Rodriguez", "Bvlgari", "Gucci", "Hermes", "Nina Ricci", "Rochas", "Viktor & Rolf", "Karl Lagerfeld", "Boucheron", "Jean Paul Gaultier" };

            foreach (var designer in designers)
            {
                await dbContext.Designers.AddAsync(new Designer { Name = designer });
            }
        }
    }
}
