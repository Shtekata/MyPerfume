namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyPerfume.Data.Models;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.InputModels;

    public interface ICategoriesService
    {
        Task AddAsync(IdAndNameDto input);

        Task<IEnumerable<T>> GetAll<T>();

        bool ExistsById(string id);

        bool ExistsByName(string name);

        Task<int> EditAsync(IdAndNameInputModel input);

        IdAndNameDto GetById(string id);

        Task<int> DeleteAsync(IdAndNameInputModel input);

        bool IsTheSameInput(IdAndNameInputModel input);
    }
}
