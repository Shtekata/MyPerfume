﻿namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyPerfume.Data.Models;
    using MyPerfume.Web.ViewModels.Dtos;

    public interface IPictureUrlsService
    {
        Task<int> AddAsync(PictureUrlDto input);

        Task<IEnumerable<T>> GetAll<T>(int? count = null);

        bool ExistsById(string id);

        bool ExistsByUrl(string id, string url);

        Task<int> EditAsync(PictureUrlDto dto);

        Task<int> EditAsync(PerfumeDto input);

        PictureUrl GetByIdModel(string id);

        PictureUrlDto GetById(string id);

        Task<int> DeleteAsync(string id);

        bool IsTheSameInput(PictureUrlDto input);

        List<SelectListItem> PictureNumbers();

        List<SelectListItem> PictureShowNumbers();

        public Dictionary<string, List<SelectListItem>> Extensions();

        int GetCount();

        Task<IEnumerable<T>> GetPage<T>(int? take = null, int skip = 0);
    }
}
