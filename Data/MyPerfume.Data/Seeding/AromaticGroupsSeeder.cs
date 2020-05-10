namespace MyPerfume.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using MyPerfume.Data.Models;

    public class AromaticGroupsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            if (dbContext.AromaticGroups.Any())
            {
                return;
            }

            var aromaticGroups = new List<string>() { "Алдехидни", "Амброви", "Ароматни", "Билкови", "Ванилови", "Водни", "Гурме", "Дървесни", "Зелени", "Землисти", "Източни", "Кожени", "Морски", "Мускусни", "Ориенталски", "Пикантни", "Плодови", "Пудрени", "Свежи", "Сладки", "Флорални", "Фужерни", "Цветни", "Цитрусови", "Шипрови" };

            foreach (var aromaticGroup in aromaticGroups)
            {
                await dbContext.AromaticGroups.AddAsync(new AromaticGroup { Name = aromaticGroup });
            }
        }
    }
}
