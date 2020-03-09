namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyPerfume.Data.Models;
    using MyPerfume.Web.ViewModels.Dto;

    public interface IDesignerService
    {
        Task AddAsync(DesignerDto input);

        Task<Designer> GetByIdWithDeletedAsync(DesignerDto input);

        bool Exists(DesignerDto input);
    }
}
