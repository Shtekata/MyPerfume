namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyPerfume.Data.Common.Repositories;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dto;

    public class PerfumesService : IPerfumesService
    {
        private readonly IDeletableEntityRepository<Perfume> perfumeRepository;

        public PerfumesService(IDeletableEntityRepository<Perfume> perfumeRepository)
        {
            this.perfumeRepository = perfumeRepository;
        }

        public async Task AddAsync(PerfumDto input)
        {
            var perfume = new Perfume
            {
                Name = input.Name,
                Description = input.Description,
                Niche = input.Niche,
                YearOfManifacture = input.YearOfManifacture,
                CustomerType = input.CustomerType,
                DesignerId = input.DesignerId,
                ColorId = input.CountryId,
                CountryId = input.CountryId,
            };

            await this.perfumeRepository.AddAsync(perfume);
            await this.perfumeRepository.SaveChangesAsync();
        }

        public IEnumerable<Designer> GetAllDesigners()
        {
            var designers = this.perfumeRepository.AllAsNoTracking().Select(x => x.Designer).ToList();
            return designers;
        }

        public IEnumerable<Color> GetAllColors()
        {
            var colors = this.perfumeRepository.AllAsNoTracking().Select(x => x.Color).ToList();
            return colors;
        }

        public IEnumerable<Country> GetAllCountries()
        {
            var countries = this.perfumeRepository.AllAsNoTracking().Select(x => x.Country).ToList();
            return countries;
        }

        public async Task<ICollection<T>> All<T>()
         => await this.perfumeRepository.AllAsNoTracking().To<T>().ToArrayAsync();
    }
}
