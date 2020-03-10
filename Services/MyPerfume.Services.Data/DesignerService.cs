namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyPerfume.Data.Common.Repositories;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Designers.InputModels;
    using MyPerfume.Web.ViewModels.Dto;

    public class DesignerService : IDesignerService
    {
        private readonly IDeletableEntityRepository<Designer> deletableEntityRepository;

        public DesignerService(IDeletableEntityRepository<Designer> deletableEntityRepository)
        {
            this.deletableEntityRepository = deletableEntityRepository;
        }

        public async Task AddAsync(CreateDesignerInputModel input)
        {
            var designer = new Designer { Name = input.Name };
            await this.deletableEntityRepository.AddAsync(designer);
            await this.deletableEntityRepository.SaveChangesAsync();
        }

        public async Task<Designer> GetByIdWithDeletedAsync(DesignerDto input)
        {
            var designer = await this.deletableEntityRepository.GetByIdWithDeletedAsync(input.Name);
            return designer;
        }

        public bool Exists(CreateDesignerInputModel input)
        {
            var designer = this.deletableEntityRepository.AllAsNoTrackingWithDeleted()
                .FirstOrDefault(x => x.Name == input.Name);
            if (designer != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllDesigners<T>()
       => await this.deletableEntityRepository.AllAsNoTracking().To<T>().ToArrayAsync();
    }
}
