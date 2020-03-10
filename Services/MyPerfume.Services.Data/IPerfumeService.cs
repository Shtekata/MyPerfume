﻿namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyPerfume.Data.Models;
    using MyPerfume.Web.ViewModels.Dto;

    public interface IPerfumeService
    {
        Task AddAsync(PerfumDto input);

        Task<ICollection<T>> All<T>();

        IEnumerable<Designer> GetAllDesigners();

        IEnumerable<Color> GetAllColors();

        IEnumerable<Country> GetAllCountries();
    }
}
