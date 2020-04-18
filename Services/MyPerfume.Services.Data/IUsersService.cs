namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyPerfume.Web.ViewModels.Dtos;

    public interface IUsersService
    {
        IEnumerable<T> All<T>();

        Task<bool> UserExists(string id);

        T GetUserById<T>(string id);

        Task EditUser(EditUserDto input);

        Task<int> DeleteUserById(string id);
    }
}
