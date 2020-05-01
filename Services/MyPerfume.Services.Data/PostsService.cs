﻿namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyPerfume.Data.Common.Repositories;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> deletableEntityRepository;

        public PostsService(IDeletableEntityRepository<Post> deletableEntityRepository)
        {
            this.deletableEntityRepository = deletableEntityRepository;
        }

        public async Task<int> AddAsync(BaseDto input)
        {
            var model = new Post { Title = input.Name };
            await this.deletableEntityRepository.AddAsync(model);
            return await this.deletableEntityRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>(int? count = null)
        {
            IQueryable<Post> query = this.deletableEntityRepository.AllAsNoTracking()
            .OrderBy(x => x.Title);
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

        public bool ExistsByTitle(string title)
        {
            var model = this.deletableEntityRepository.AllAsNoTracking()
                 .FirstOrDefault(x => x.Title == title);
            if (model != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> EditAsync(BaseDto input)
        {
            var model = this.deletableEntityRepository.All()
                 .FirstOrDefault(x => x.Id == input.Id);

            if (model == null)
            {
                return 0;
            }

            model.Title = input.Name;
            return await this.deletableEntityRepository.SaveChangesAsync();
        }

        public Post GetByIdModel(string id)
        {
            return this.deletableEntityRepository.AllAsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public BaseDto GetById(string id)
        {
            var model = this.GetByIdModel(id);

            var dto = AutoMapperConfig.MapperInstance.Map<BaseDto>(model);
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

        public bool IsTheSameInput(BaseDto input)
        {
            var model = this.deletableEntityRepository.All()
                .Where(x => x.Id == input.Id)
                .FirstOrDefault();

            return input.Name == model.Title;
        }

        public int GetCount()
        {
            return this.deletableEntityRepository.All().Count();
        }
    }
}
