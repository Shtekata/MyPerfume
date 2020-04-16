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

    public class DesignersService : IDesignersService
    {
        private readonly IDeletableEntityRepository<Designer> deletableEntityRepository;

        public DesignersService(IDeletableEntityRepository<Designer> deletableEntityRepository)
        {
            this.deletableEntityRepository = deletableEntityRepository;
        }

        public async Task AddAsync(DesignerDto input)
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

        public async Task<IEnumerable<T>> GetAllDesigners<T>()
       => await this.deletableEntityRepository.AllAsNoTracking()
            .OrderBy(x => x.Name)
            .To<T>()
            .ToArrayAsync();

        public bool Exists(string id)
        {
            var designer = this.deletableEntityRepository.AllAsNoTrackingWithDeleted()
                 .FirstOrDefault(x => x.Id == id);
            if (designer != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> EditAsync(DesignerInputModel input)
        {
            var designer = this.deletableEntityRepository.All()
                 .FirstOrDefault(x => x.Id == input.Id);

            designer.Name = input.Name;
            return await this.deletableEntityRepository.SaveChangesAsync();
        }

        public DesignerDto GetById(string id)
        {
            var designer = this.deletableEntityRepository.AllAsNoTracking()
                 .FirstOrDefault(x => x.Id == id);

            var designerDto = AutoMapperConfig.MapperInstance.Map<DesignerDto>(designer);
            return designerDto;
        }

        public async Task<int> DeleteAsync(DesignerInputModel input)
        {
            var designer = this.deletableEntityRepository.All()
                 .FirstOrDefault(x => x.Id == input.Id);

            this.deletableEntityRepository.Delete(designer);
            return await this.deletableEntityRepository.SaveChangesAsync();
        }
    }
}
