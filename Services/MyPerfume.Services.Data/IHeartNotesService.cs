﻿namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyPerfume.Web.ViewModels.Dtos;

    public interface IHeartNotesService
    {
        Task<int> AddAsync(BaseDto input);

        Task<IEnumerable<T>> GetAll<T>(int? count = null);

        bool ExistsById(string id);

        bool ExistsByName(string name);

        Task<int> EditAsync(BaseDto input);

        BaseDto GetById(string id);

        Task<int> DeleteAsync(string id);

        bool IsTheSameInput(BaseDto input);

        int GetCount();
    }
}
