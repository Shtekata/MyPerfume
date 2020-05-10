namespace MyPerfume.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using MyPerfume.Data.Models;

    public class PerfumePictureUrlsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            if (dbContext.PerfumesPictureUrls.Any())
            {
                return;
            }

            var perfumePictureUrls = new List<(string PerfumeId, string PictureUrlId)>()
            {
                ("string", "string"),
                ("string", "string"),
                ("string", "string"),
            };

            foreach (var perfumePictureUrlStrings in perfumePictureUrls)
            {
                var perfumePictureUrl = new PerfumePictureUrl
                {
                    PerfumeId = perfumePictureUrlStrings.PerfumeId,
                    PictureUrlId = perfumePictureUrlStrings.PictureUrlId,
                };

                await dbContext.PerfumesPictureUrls.AddAsync(perfumePictureUrl);
            }
        }
    }
}
