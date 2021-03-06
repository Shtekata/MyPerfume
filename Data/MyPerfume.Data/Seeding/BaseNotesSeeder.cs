﻿namespace MyPerfume.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using MyPerfume.Data.Models;

    public class BaseNotesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            if (dbContext.BaseNotes.Any())
            {
                return;
            }

            var baseNotes = new List<string>() { "Амбра", "Амбрета", "Амброксан", "Амбър", "Атласки кедър", "Бадеми", "Балсам Толу", "Бензоин", "Божур", "Бърбън ванилия", "Бял амбър", "Бял кедър", "Бял мускус", "Бяло дърво", "Ванилия", "Велур", "Ветивер", "Вирджински Кедър", "Джинджифил", "Джинджифилов бисквит", "Дъбов мъх", "Дъбова кора", "Дървесен акорд", "Дървесина", "Дървесни нотки", "Жасмин", "Женско биле", "Захар", "Здравец", "Зърна тонка", "Ирис", "Какао", "Какао", "Карамел", "Каршмеран", "Кафе", "Кашмирово дърво", "Кедър", "Кедър от Вирджиния", "Кестен", "Кехлибар", "Кожа", "Кумарин", "Лабданум", "Майска роза", "Махагон", "Мед", "Мимоза", "Мира", "Мускус", "Мъх", "Олибан", "Ориз", "Орхидея", "Палисандрово дърво", "Палисандър", "Папирус", "Пачули", "Перуанския балсам", "Пралина", "Праскова", "Прозрачен мускус", "Пудрови нотки", "Сандал", "Сандалово дърво", "Светла дървесина", "Сиамски тамян", "Сива амбра", "Слива", "Смола", "Сушени плодове", "Тамян", "Теменужка", "Тик", "Тютюн", "Хедион", "Хелиотроп", "Циклозал", "Череша", "Шоколад" };

            foreach (var baseNote in baseNotes)
            {
                await dbContext.BaseNotes.AddAsync(new BaseNote { Name = baseNote });
            }
        }
    }
}
