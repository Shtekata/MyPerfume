namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyPerfume.Data.Models;
    using MyPerfume.Web.ViewModels.Designers.InputModels;
    using MyPerfume.Web.ViewModels.Dto;

    public interface IDesignerService
    {
        Task AddAsync(CreateDesignerInputModel input);

        Task<Designer> GetByIdWithDeletedAsync(DesignerDto input);

        bool Exists(CreateDesignerInputModel input);

        Task<IEnumerable<T>> GetAllDesigners<T>();
    }
}
