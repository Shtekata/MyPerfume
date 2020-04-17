namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyPerfume.Data.Models;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.InputModels;

    public interface IPictureUrlsService
    {
        Task AddAsync(PictureUrlDto input);

        Task<IEnumerable<T>> GetAll<T>();

        bool ExistsById(string id);

        bool ExistsByName(string url);

        Task<int> EditAsync(PictureUrlInputModel input);

        PictureUrlDto GetById(string id);

        Task<int> DeleteAsync(PictureUrlInputModel input);

        bool IsTheSameInput(PictureUrlInputModel input);
    }
}
