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

    public class PerfumesService : IPerfumesService
    {
        private readonly IDeletableEntityRepository<Perfume> deletableEntityRepository;
        private readonly IDesignersService designersService;
        private readonly ICountriesService countriesService;
        private readonly IColorsService colorsService;

        public PerfumesService(
            IDeletableEntityRepository<Perfume> deletableEntityRepository,
            IDesignersService designersService,
            ICountriesService countriesService,
            IColorsService colorsService)
        {
            this.deletableEntityRepository = deletableEntityRepository;
            this.designersService = designersService;
            this.countriesService = countriesService;
            this.colorsService = colorsService;
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
            var model = this.deletableEntityRepository.AllAsNoTracking()
                 .FirstOrDefault(x => x.Id == id);

            var dto = AutoMapperConfig.MapperInstance.Map<PerfumeDto>(model);
            return dto;
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

            var a = model.ColorId;

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

            return result;
        }
    }
}
