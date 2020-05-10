namespace MyPerfume.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using MyPerfume.Data.Models;

    public class CountriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            if (dbContext.Countries.Any())
            {
                return;
            }

            var countries = new List<string>() { "Австралия", "Австрия", "Белгия", "България", "Великобритания", "Германия", "Гърция", "Дания", "Европейски Съюз", "Ирландия", "Испания", "Италия", "Канада", "Китай", "Мексико", "ОАЕ", "Оман", "Полша", "Русия", "САЩ", "Турция", "Финландия", "Франция", "Холандия", "Чехия", "Швейцария", "Швеция", "Япония" };

            foreach (var country in countries)
            {
                await dbContext.Countries.AddAsync(new Country { Name = country });
            }
        }
    }
}
