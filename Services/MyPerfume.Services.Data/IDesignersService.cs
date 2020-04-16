namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyPerfume.Data.Models;
    using MyPerfume.Web.ViewModels.Designers.InputModels;
    using MyPerfume.Web.ViewModels.Dto;

    public interface IDesignersService
    {
        Task AddAsync(DesignerDto input);

        Task<Designer> GetByIdWithDeletedAsync(DesignerDto input);

        Task<IEnumerable<T>> GetAllDesigners<T>();

        bool Exists(string id);

        Task<int> EditAsync(DesignerInputModel input);

        DesignerDto GetById(string id);

        Task<int> DeleteAsync(DesignerInputModel input);
    }
}
