namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyPerfume.Web.ViewModels.Dtos;

    public interface IPerfumesService
    {
        Task AddAsync(PerfumeDto input);

        Task<IEnumerable<T>> GetAll<T>();

        bool ExistsById(string id);

        bool ExistsByName(string name);

        Task<int> EditAsync(PerfumeDto input);

        PerfumeDto GetById(string id);

        Task<int> DeleteAsync(string id);

        bool IsTheSameInput(PerfumeDto input);

        Task<Dictionary<string, List<SelectListItem>>> Extensions();
    }
}
