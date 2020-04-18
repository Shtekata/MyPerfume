namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyPerfume.Web.ViewModels.Dtos;

    public interface IPictureUrlsService
    {
        Task AddAsync(PictureUrlDto input);

        Task<IEnumerable<T>> GetAll<T>();

        bool ExistsById(string id);

        bool ExistsByName(string url);

        Task<int> EditAsync(PictureUrlDto input);

        PictureUrlDto GetById(string id);

        Task<int> DeleteAsync(string id);

        bool IsTheSameInput(PictureUrlDto input);
    }
}
