namespace MyPerfume.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices.ComTypes;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyPerfume.Data.Common.Repositories;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;
    using MyPerfume.Web.ViewModels.InputModels;

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

        public async Task AddAsync(PerfumeDto input)
        {
            var model = AutoMapperConfig.MapperInstance.Map<Perfume>(input);
            model.Id = Guid.NewGuid().ToString();

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

        public async Task<int> EditAsync(PerfumeDto input)
        {
            var model = this.deletableEntityRepository.All()
                 .FirstOrDefault(x => x.Id == input.Id);

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

        public PerfumeDto GetById(string id)
        {
            var model = this.deletableEntityRepository.All()
                 .FirstOrDefault(x => x.Id == id);

            var customDto = this.deletableEntityRepository.All()
                .Where(x => x.Id == id)
                .Select(x => new PerfumeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ColorId = x.ColorId,
                    CountryId = x.CountryId,
                    DesignerId = x.DesignerId,
                    CustomerType = x.CustomerType,
                    YearOfManifacture = (int)x.YearOfManifacture,
                    ColorName = x.Color.Name,
                    Niche = x.Niche,
                    Description = x.Description,
                    PictureUrls = x.PictureUrls.Select(y => new PictureUrlCollectionModel
                    {
                        Id = y.Id,
                        DesignerAndPerfumeNames = y.DesignerAndPerfumeNames,
                    }),
                }).FirstOrDefault();

            var dto = AutoMapperConfig.MapperInstance.Map<PerfumeDto>(model);
            return customDto;
        }

        public async Task<int> DeleteAsync(string id)
        {
            var model = this.deletableEntityRepository.All()
                 .FirstOrDefault(x => x.Id == id);

            this.deletableEntityRepository.Delete(model);
            return await this.deletableEntityRepository.SaveChangesAsync();
        }

        public bool IsTheSameInput(PerfumeDto input)
        {
            var model = this.deletableEntityRepository.All()
                .Where(x => x.Id == input.Id)
                .FirstOrDefault();

            var inputPictureUrls = input.PictureUrls.Where(x => x.IsSelected == true)
                .OrderBy(x => x.Id)
                .ToArray();
            var modelUrls = this.deletableEntityRepository.All()
                .Where(x => x.Id == input.Id)
                .Select(x => new Perfume
                {
                    PictureUrls = x.PictureUrls.Select(y => new PictureUrl
                    {
                        Id = y.Id,
                    }),
                }).FirstOrDefault();
            var modelPictureUrls = modelUrls.PictureUrls.OrderBy(x => x.Id).ToArray();

            if (inputPictureUrls.Count() != modelPictureUrls.Count())
            {
                return false;
            }

            for (int i = 0; i < inputPictureUrls.Count(); i++)
            {
                if (inputPictureUrls[i].Id != modelPictureUrls[i].Id)
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
            var pictureUrls = pictureUrlsModel.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.DesignerAndPerfumeNames} : снимка № {x.PictureNumber}",
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
    }
}
