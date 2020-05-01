namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyPerfume.Data.Models;
    using MyPerfume.Web.ViewModels.Dtos;

    public interface IPerfumesService
    {
        Task<string> AddAsync(PerfumeAddDto input);

        Task<IEnumerable<T>> GetAll<T>(int? count = null);

        bool ExistsById(string id);

        bool ExistsByName(string name);

        Task<int> EditAsync(PerfumeDto input);

        Perfume GetByIdModel(string id);

        PerfumeDto GetById(string id);

        T GetByName<T>(string name);

        Task<int> DeleteAsync(string id);

        bool IsTheSameInput(PerfumeDto input);

        Task<Dictionary<string, List<SelectListItem>>> Extensions();

        int GetCount();

        Task<IEnumerable<T>> GetPage<T>(int? take = null, int skip = 0);
    }
}
