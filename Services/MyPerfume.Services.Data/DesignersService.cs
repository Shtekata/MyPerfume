namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyPerfume.Data.Common.Repositories;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.InputModels;

    public class DesignersService : IDesignersService
    {
        private readonly IDeletableEntityRepository<Designer> deletableEntityRepository;

        public DesignersService(IDeletableEntityRepository<Designer> deletableEntityRepository)
        {
            this.deletableEntityRepository = deletableEntityRepository;
        }

        public async Task AddAsync(IdAndNameDto input)
        {
            var model = new Designer { Name = input.Name };
            await this.deletableEntityRepository.AddAsync(model);
            await this.deletableEntityRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>()
       => await this.deletableEntityRepository.AllAsNoTracking()
            .OrderBy(x => x.Name)
            .To<T>()
            .ToArrayAsync();

        public bool ExistsById(string id)
        {
            var model = this.deletableEntityRepository.AllAsNoTrackingWithDeleted()
                 .FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExistsByName(string name)
        {
            var model = this.deletableEntityRepository.AllAsNoTracking()
                 .FirstOrDefault(x => x.Name == name);
            if (model != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> EditAsync(IdAndNameInputModel input)
        {
            var model = this.deletableEntityRepository.All()
                 .FirstOrDefault(x => x.Id == input.Id);

            model.Name = input.Name;
            return await this.deletableEntityRepository.SaveChangesAsync();
        }

        public IdAndNameDto GetById(string id)
        {
            var model = this.deletableEntityRepository.AllAsNoTracking()
                 .FirstOrDefault(x => x.Id == id);

            var dto = AutoMapperConfig.MapperInstance.Map<IdAndNameDto>(model);
            return dto;
        }

        public async Task<int> DeleteAsync(IdAndNameInputModel input)
        {
            var model = this.deletableEntityRepository.All()
                 .FirstOrDefault(x => x.Id == input.Id);

            this.deletableEntityRepository.Delete(model);
            return await this.deletableEntityRepository.SaveChangesAsync();
        }

        public bool IsTheSameInput(IdAndNameInputModel input)
        {
            var model = this.deletableEntityRepository.All()
                .Where(x => x.Id == input.Id)
                .FirstOrDefault();

            return input.Name == model.Name;
        }
    }
}
