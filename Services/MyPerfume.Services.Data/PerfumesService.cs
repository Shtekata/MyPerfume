namespace MyPerfume.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyPerfume.Data.Common.Repositories;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.ViewModels;

    public class PerfumesService : IPerfumesService
    {
        private readonly IDeletableEntityRepository<Perfume> deletableEntityRepository;
        private readonly IDesignersService designersService;
        private readonly ICountriesService countriesService;
        private readonly IColorsService colorsService;
        private readonly IPictureUrlsService pictureUrlsService;

        public PerfumesService(
            IDeletableEntityRepository<Perfume> deletableEntityRepository,
            IDesignersService designersService,
            ICountriesService countriesService,
            IColorsService colorsService,
            IPictureUrlsService pictureUrlsService)
        {
            this.deletableEntityRepository = deletableEntityRepository;
            this.designersService = designersService;
            this.countriesService = countriesService;
            this.colorsService = colorsService;
            this.pictureUrlsService = pictureUrlsService;
        }

        public async Task<string> AddAsync(PerfumeAddDto input)
        {
            var model = AutoMapperConfig.MapperInstance.Map<Perfume>(input);
            model.Id = Guid.NewGuid().ToString();

            await this.deletableEntityRepository.AddAsync(model);
            await this.deletableEntityRepository.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<T>> GetAll<T>(int? count = null)
        {
            IQueryable<Perfume> query = this.deletableEntityRepository.AllAsNoTracking()
            .OrderBy(x => x.Name);
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

        public async Task<int> EditAsync(PerfumeDto input)
        {
            var model = this.deletableEntityRepository.All()
                 .FirstOrDefault(x => x.Id == input.Id);

            if (model == null)
            {
                return 0;
            }

            model.Name = input.Name;
            model.ColorId = input.ColorId;
            model.CountryId = input.CountryId;
            model.DesignerId = input.DesignerId;
            model.CustomerType = input.CustomerType;
            model.Niche = input.Niche;
            model.YearOfManifacture = input.YearOfManifacture;
            model.Description = input.Description;

            // model.PerfumesTopNotes.SelectMany(x=>x.Id) = input.TopNotes;
            // model.PerfumesHeartNotes = input.PerfumesHeartNotes;
            // model.PerfumesBaseNotes = input.PerfumesBaseNotes;
            // model.PerfumesAromaticGroups = input.PerfumesAromaticGroups;
            // model.PerfumesCategories = input.PerfumesCategories;
            // model.PerfumesPerfumers = input.PerfumesPerfumers;
            // model.PerfumesPurposes = input.PerfumesPurposes;
            // model.PerfumesSeasons = input.PerfumesSeasons;
            // model.PictureUrls = input.PictureUrls;
            // model.YearOfManifacture = input.YearOfManifacture;
            return await this.deletableEntityRepository.SaveChangesAsync();
        }

        public Perfume GetByIdModel(string id)
        {
            return this.deletableEntityRepository.All()
                .FirstOrDefault(x => x.Id == id);
        }

        public T GetById<T>(string id)
        {
            var model = this.deletableEntityRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return model;
        }

        public T GetByName<T>(string name)
        {
            var modifiedName = name.Replace('-', ' ');
            var model = this.deletableEntityRepository.All()
                .Where(x => x.Name == modifiedName)
                .To<T>()
                .FirstOrDefault();

            return model;
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

        public bool IsTheSameInput(PerfumeDto input)
        {
            var model = this.deletableEntityRepository.All()
                .Where(x => x.Id == input.Id)
                .FirstOrDefault();

            var inputPictureUrls = input.Extensions["PictureUrls"].Where(x => x.Selected == true)
                .OrderBy(x => x.Value)
                .Select(x => x.Value)
                .ToList();
            var modelUrls = this.deletableEntityRepository.All()
                .Where(x => x.Id == input.Id)
                .Select(x => new PerfumeDto
                {
                    PictureUrls = x.PerfumesPictureUrls.Select(y => new PictureUrlViewModel
                    {
                        Id = y.PictureUrlId,
                    }),
                }).FirstOrDefault();
            var modelPictureUrls = modelUrls.PictureUrls
                .OrderBy(x => x.Id)
                .Select(x => x.Id)
                .ToList();

            if (inputPictureUrls.Count() != modelPictureUrls.Count())
            {
                return false;
            }

            for (int i = 0; i < inputPictureUrls.Count(); i++)
            {
                if (inputPictureUrls[i] != modelPictureUrls[i])
                {
                    return false;
                }
            }

            return model.Name == input.Name &&
                model.ColorId == input.ColorId &&
                model.CountryId == input.CountryId &&
                model.DesignerId == input.DesignerId &&
                model.CustomerType == input.CustomerType &&
                model.Niche == input.Niche &&
                model.YearOfManifacture == input.YearOfManifacture &&
                model.Description == input.Description;

            // input.AromaticGroups == model.PerfumesAromaticGroups &&
            // input.BaseNotes == model.PerfumesBaseNotes &&
            // input.Categories == model.PerfumesCategories &&
            // input.HeartNotes == model.PerfumesHeartNotes &&
            // input.Perfumers == model.PerfumesPerfumers &&
            // input.PerfumesPurposes == model.PerfumesPurposes &&
            // input.PerfumesSeasons == model.PerfumesSeasons &&
            // input.TopNotes == model.PerfumesTopNotes &&
            // input.PictureUrls == model.PictureUrls &&
        }

        public async Task<Dictionary<string, List<SelectListItem>>> Extensions()
        {
            var designersModel = await this.designersService.GetAll<BaseDto>();
            var designers = designersModel.Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Name,
            }).ToList();

            var colorsModel = await this.colorsService.GetAll<BaseDto>();
            var colors = colorsModel.Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Name,
            }).ToList();

            var countriesModel = await this.countriesService.GetAll<BaseDto>();
            var countries = countriesModel.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).ToList();

            var pictureUrlsModel = await this.pictureUrlsService.GetAll<PictureUrlDto>();
            var pictureUrls = pictureUrlsModel
                .OrderBy(x => x.DesignerName)
                .ThenBy(x => x.PerfumeName)
                .ThenBy(x => x.PictureNumber)
                .ThenBy(x => x.PictureShowNumber)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.DesignerName}/{x.PerfumeName}/Picture № {x.PictureNumber}/PShow № {x.PictureShowNumber}",
                }).ToList();

            var years = new List<SelectListItem>();
            for (int i = 1900; i < 2050; i++)
            {
                var year = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString(),
                };
                years.Add(year);
            }

            var result = new Dictionary<string, List<SelectListItem>>();
            result["Designers"] = designers;
            result["Colors"] = colors;
            result["Countries"] = countries;
            result["Years"] = years;
            result["PictureUrls"] = pictureUrls;

            return result;
        }

        public async Task<Dictionary<string, List<SelectListItem>>> Extensions(string id)
        {
            var extensions = await this.Extensions();

            var pictureUrlsAll = await this.pictureUrlsService.GetAll<PictureUrlDto>();
            var pictureUrlsPerfumeModel = this.GetById<PictureUrlServiceModel>(id);
            var pictureUrlsPerfume = string.Empty;

            foreach (var pictureId in pictureUrlsPerfumeModel.PictureUrls)
            {
                pictureUrlsPerfume = pictureUrlsPerfume + pictureId + ',';
            }

            foreach (var picture in pictureUrlsAll)
            {
                if (pictureUrlsPerfume.Contains(picture.Id))
                {
                    picture.IsSelected = true;
                }
            }

            var pictureUrls = pictureUrlsAll
                .OrderBy(x => x.DesignerName)
                .ThenBy(x => x.PerfumeName)
                .ThenBy(x => x.PictureNumber)
                .ThenBy(x => x.PictureShowNumber)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.DesignerName}/{x.PerfumeName}/Picture № {x.PictureNumber}/PShow № {x.PictureShowNumber}",
                    Selected = x.IsSelected,
                }).ToList();
            extensions["PictureUrls"] = pictureUrls;

            return extensions;
        }

        public int GetCount()
        {
            return this.deletableEntityRepository.All().Count();
        }

        public async Task<IEnumerable<T>> GetPage<T>(int? take = null, int skip = 0)
        {
            var query = this.deletableEntityRepository.All()
                .OrderBy(x => x.Name)
                .Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.To<T>().ToArrayAsync();
        }
    }
}
