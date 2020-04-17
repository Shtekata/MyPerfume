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

    public class PictureUrlsService : IPictureUrlsService
    {
        private readonly IDeletableEntityRepository<PictureUrl> deletableEntityRepository;

        public PictureUrlsService(IDeletableEntityRepository<PictureUrl> deletableEntityRepository)
        {
            this.deletableEntityRepository = deletableEntityRepository;
        }

        public async Task AddAsync(PictureUrlDto input)
        {
            var model = new PictureUrl
            {
                Url = input.Url,
                DesignerAndPerfumeNames = input.DesignerAndPerfumeNames,
                PictureNumber = input.PictureNumber,
            };
            await this.deletableEntityRepository.AddAsync(model);
            await this.deletableEntityRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>()
       => await this.deletableEntityRepository.AllAsNoTracking()
            .OrderBy(x => x.DesignerAndPerfumeNames)
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

        public bool ExistsByName(string url)
        {
            var model = this.deletableEntityRepository.AllAsNoTracking()
                 .FirstOrDefault(x => x.Url == url);
            if (model != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> EditAsync(PictureUrlInputModel input)
        {
            var model = this.deletableEntityRepository.All()
                 .FirstOrDefault(x => x.Id == input.Id);

            model.Url = input.Url;
            model.DesignerAndPerfumeNames = input.DesignerAndPerfumeNames;
            model.PictureNumber = input.PictureNumber;
            return await this.deletableEntityRepository.SaveChangesAsync();
        }

        public PictureUrlDto GetById(string id)
        {
            var model = this.deletableEntityRepository.AllAsNoTracking()
                 .FirstOrDefault(x => x.Id == id);

            var dto = AutoMapperConfig.MapperInstance.Map<PictureUrlDto>(model);
            return dto;
        }

        public async Task<int> DeleteAsync(PictureUrlInputModel input)
        {
            var model = this.deletableEntityRepository.All()
                 .FirstOrDefault(x => x.Id == input.Id);

            this.deletableEntityRepository.Delete(model);
            return await this.deletableEntityRepository.SaveChangesAsync();
        }

        public bool IsTheSameInput(PictureUrlInputModel input)
        {
            var model = this.deletableEntityRepository.All()
                .Where(x => x.Id == input.Id)
                .FirstOrDefault();

            return input.Url == model.Url &&
                input.DesignerAndPerfumeNames == model.DesignerAndPerfumeNames &&
                input.PictureNumber == model.PictureNumber;
        }
    }
}
