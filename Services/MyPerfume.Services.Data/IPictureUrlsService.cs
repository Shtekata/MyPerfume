namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyPerfume.Web.ViewModels.Dtos;

    public interface IPictureUrlsService
    {
        Task<int> AddAsync(PictureUrlDto input);

        Task<IEnumerable<T>> GetAll<T>();

        bool ExistsById(string id);

        bool ExistsByName(string url);

        Task<int> EditAsync(PictureUrlDto dto);

        Task<int> EditAsync(PerfumeDto input);

        PictureUrlDto GetById(string id);

        Task<int> DeleteAsync(string id);

        bool IsTheSameInput(PictureUrlDto input);

        bool GetByPerfumeAndPictureUrlId(string perfumeId, string pictureUrlId);

        IList<T> GetPerfumePictures<T>();
    }
}
