namespace MyPerfume.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<int> AddAsync(PictureUrlDto input)
        {
            var model = AutoMapperConfig.MapperInstance.Map<PictureUrl>(input);
            model.Id = Guid.NewGuid().ToString();
            await this.deletableEntityRepository.AddAsync(model);
            return await this.deletableEntityRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>(int? count = null)
        {
            IQueryable<PictureUrl> query = this.deletableEntityRepository.AllAsNoTracking()
            .OrderBy(x => x.DesignerAndPerfumeNames);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return await query.To<T>()
            .ToListAsync();
        }

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

        public bool ExistsByUrl(string url)
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

        public async Task<int> EditAsync(PictureUrlDto dto)
        {
            var model = this.deletableEntityRepository.All()
                .Where(x => x.Id == dto.Id)
                .FirstOrDefault();
            model.DesignerAndPerfumeNames = dto.DesignerAndPerfumeNames;
            model.PictureNumber = dto.PictureNumber;
            model.Url = dto.Url;
            model.PictureShowNumber = dto.PictureShowNumber;
            return await this.deletableEntityRepository.SaveChangesAsync();
        }

        public async Task<int> EditAsync(PerfumeDto input)
        {
            var perfumId = input.Id;
            var pictureUrls = input.PictureUrls.ToArray();
            for (var i = 0; i < pictureUrls.Count(); i++)
            {
                var result = 0;

                var pictureUrlModel = this.deletableEntityRepository.All()
                    .FirstOrDefault(x => x.Id == pictureUrls[i].Id);

                if (pictureUrlModel == null)
                {
                    return 0;
                }

                var isAlreadyHaveThisPicture = pictureUrlModel.PerfumeId == perfumId;
                if (pictureUrls[i].IsSelected && !isAlreadyHaveThisPicture)
                {
                    pictureUrlModel.PerfumeId = perfumId;
                    result = await this.deletableEntityRepository.SaveChangesAsync();
                }
                else if (!pictureUrls[i].IsSelected && isAlreadyHaveThisPicture)
                {
                    pictureUrlModel.PerfumeId = null;
                    result = await this.deletableEntityRepository.SaveChangesAsync();
                }
                else
                {
                    continue;
                }

                if (result == 1)
                {
                    if (i < input.PictureUrls.Count())
                    {
                        continue;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            return 1;
        }

        public PictureUrlDto GetById(string id)
        {
            var model = this.deletableEntityRepository.AllAsNoTracking()
                 .FirstOrDefault(x => x.Id == id);

            var dto = AutoMapperConfig.MapperInstance.Map<PictureUrlDto>(model);
            return dto;
        }

        public async Task<int> DeleteAsync(string id)
        {
            var model = this.deletableEntityRepository.All()
                 .FirstOrDefault(x => x.Id == id);

            if (model == null)
            {
                return 0;
            }

            this.deletableEntityRepository.Delete(model);
            return await this.deletableEntityRepository.SaveChangesAsync();
        }

        public bool IsTheSameInput(PictureUrlDto input)
        {
            var model = this.deletableEntityRepository.All()
                .Where(x => x.Id == input.Id)
                .FirstOrDefault();

            return
                input.DesignerAndPerfumeNames == model.DesignerAndPerfumeNames &&
                input.PictureNumber == model.PictureNumber &&
                input.PictureShowNumber == model.PictureShowNumber;
        }

        public bool GetByPerfumeAndPictureUrlId(string perfumeId, string pictureUrlIdInput)
        {
            var result = this.deletableEntityRepository.All()
                .Any(x => x.PerfumeId == perfumeId && x.Id == pictureUrlIdInput);

            return result;
        }

        public IList<T> GetPerfumePictures<T>()
        {
            var pictureUrls = this.deletableEntityRepository.AllAsNoTracking()
                .OrderBy(x => x.DesignerAndPerfumeNames)
                .ThenBy(x => x.PictureNumber)
                .To<T>()
                .ToList();

            return pictureUrls;
        }

        public List<SelectListItem> PictureNumbers()
        {
            var pictureNumbers = new List<SelectListItem>();
            for (int i = 1; i < 16; i++)
            {
                var pictureNumber = new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                };

                pictureNumbers.Add(pictureNumber);
            }

            return pictureNumbers;
        }

        public List<SelectListItem> PictureShowNumbers()
        {
            var pictureNumbers = new List<SelectListItem>();
            for (int i = 1; i < 7; i++)
            {
                var pictureNumber = new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                };

                pictureNumbers.Add(pictureNumber);
            }

            return pictureNumbers;
        }

        public int GetCount()
        {
            return this.deletableEntityRepository.All().Count();
        }

        public async Task<IEnumerable<T>> GetPage<T>(int? take = null, int skip = 0)
        {
            var query = this.deletableEntityRepository.All()
                .OrderBy(x => x.DesignerAndPerfumeNames)
                .Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.To<T>().ToArrayAsync();
        }
    }
}
