namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyPerfume.Data.Models;
    using MyPerfume.Web.ViewModels.Dtos;

    public interface IPostsService
    {
        Task<int> AddAsync(PostDto input);

        Task<IEnumerable<T>> GetAll<T>(int? count = null);

        bool ExistsById(string id);

        bool ExistsByTitle(string title);

        Task<int> EditAsync(PostDto input);

        Post GetByIdModel(string id);

        PostDto GetById(string id);

        Task<int> DeleteAsync(string id);

        bool IsTheSameInput(PostDto input);

        int GetCount();
    }
}
