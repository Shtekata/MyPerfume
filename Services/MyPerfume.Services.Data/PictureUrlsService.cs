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
    using MyPerfume.Web.ViewModels.ViewModels;

    public class PictureUrlsService : IPictureUrlsService
    {
        private readonly IDeletableEntityRepository<PictureUrl> deletableEntityRepository;
        private readonly IDeletableEntityRepository<PerfumePictureUrl> deletablePerfumePictureUrlRepository;
        private readonly IDeletableEntityRepository<Perfume> deletablePerfumeEntityRepository;
        private readonly IDeletableEntityRepository<Designer> deletableDesignerEntityRepository;

        public PictureUrlsService(
            IDeletableEntityRepository<PictureUrl> deletableEntityRepository,
            IDeletableEntityRepository<PerfumePictureUrl> deletablePerfumePictureUrlRepository,
            IDeletableEntityRepository<Perfume> deletablePerfumeEntityRepository,
            IDeletableEntityRepository<Designer> deletableDesignerEntityRepository)
        {
            this.deletableEntityRepository = deletableEntityRepository;
            this.deletablePerfumePictureUrlRepository = deletablePerfumePictureUrlRepository;
            this.deletablePerfumeEntityRepository = deletablePerfumeEntityRepository;
            this.deletableDesignerEntityRepository = deletableDesignerEntityRepository;
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
            .OrderBy(x => x.DesignerName)
            .ThenBy(x => x.PerfumeName)
            .ThenBy(x => x.AdditionalInformation)
            .ThenBy(x => x.PictureNumber)
            .ThenBy(x => x.PictureShowNumber);
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

        public bool ExistsByUrl(string id, string url)
        {
            var model = this.deletableEntityRepository.AllAsNoTracking()
                 .FirstOrDefault(x => x.Url == url);
            if (model != null && model.Id != id)
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
            if (model == null)
            {
                return 0;
            }

            model.Url = dto.Url;
            model.DesignerName = dto.DesignerName;
            model.PerfumeName = dto.PerfumeName;
            model.AdditionalInformation = dto.AdditionalInformation;
            model.PictureNumber = dto.PictureNumber;
            model.PictureShowNumber = dto.PictureShowNumber;
            return await this.deletableEntityRepository.SaveChangesAsync();
        }

        public async Task<int> EditAsync(PerfumeDto input)
        {
            var pictureUrls = input.Extensions["PictureUrls"];

            var perfumPictureUrls = this.deletablePerfumeEntityRepository.All()
                .Where(x => x.Id == input.Id)
                .To<PictureUrlServiceModel>()
                .FirstOrDefault();
            var perfumPictureUrlsString = string.Empty;
            foreach (var pictureId in perfumPictureUrls.PictureUrls)
            {
                perfumPictureUrlsString = perfumPictureUrlsString + pictureId + ',';
            }

            for (var i = 0; i < pictureUrls.Count(); i++)
            {
                var result = 0;

                var isAlreadyHaveThisPicture = perfumPictureUrlsString.Contains(pictureUrls[i].Value);
                if (pictureUrls[i].Selected && !isAlreadyHaveThisPicture)
                {
                    var perfumePictureUrl = new PerfumePictureUrl
                    {
                        PerfumeId = input.Id,
                        PictureUrlId = pictureUrls[i].Value,
                    };
                    await this.deletablePerfumePictureUrlRepository.AddAsync(perfumePictureUrl);
                    result = await this.deletablePerfumePictureUrlRepository.SaveChangesAsync();
                }
                else if (!pictureUrls[i].Selected && isAlreadyHaveThisPicture)
                {
                    var perfumePictureUrl = this.deletablePerfumePictureUrlRepository.All()
                        .FirstOrDefault(x => x.PerfumeId == input.Id && x.PictureUrlId == pictureUrls[i].Value);
                    this.deletablePerfumePictureUrlRepository.Delete(perfumePictureUrl);

                    result = await this.deletablePerfumePictureUrlRepository.SaveChangesAsync();
                }
                else
                {
                    continue;
                }

                if (result == 1)
                {
                    if (i < pictureUrls.Count())
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

        public PictureUrl GetByIdModel(string id)
        {
            return this.deletableEntityRepository.AllAsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public PictureUrlDto GetById(string id)
        {
            var model = this.GetByIdModel(id);

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
                input.DesignerName == model.DesignerName &&
                input.PerfumeName == model.PerfumeName &&
                input.PictureNumber == model.PictureNumber &&
                input.PictureShowNumber == model.PictureShowNumber;
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

        public Dictionary<string, List<SelectListItem>> Extensions()
        {
            var result = new Dictionary<string, List<SelectListItem>>();
            result["PictureNumbers"] = this.PictureNumbers();
            result["PictureShowNumbers"] = this.PictureShowNumbers();
            result["Designers"] = this.deletableDesignerEntityRepository.AllAsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name,
                }).ToList();
            result["Perfumes"] = this.deletablePerfumeEntityRepository.AllAsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name,
                }).ToList();
            result["Perfumes"].Add(new SelectListItem
            {
                Value = "Common",
                Text = "Common",
            });

            return result;
        }

        public int GetCount()
        {
            return this.deletableEntityRepository.All().Count();
        }

        public async Task<IEnumerable<T>> GetPage<T>(int? take = null, int skip = 0)
        {
            var query = this.deletableEntityRepository.All()
                .OrderBy(x => x.DesignerName)
                .ThenBy(x => x.PerfumeName)
                .ThenBy(x => x.AdditionalInformation)
                .ThenBy(x => x.PictureNumber)
                .Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.To<T>().ToArrayAsync();
        }
    }
}
