namespace MyPerfume.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using MyPerfume.Data.Common.Repositories;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Dtos;

    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationUser> deletableEntityRepository;

        public UsersService(UserManager<ApplicationUser> userManager, IDeletableEntityRepository<ApplicationUser> deletableEntityRepository)
        {
            this.userManager = userManager;
            this.deletableEntityRepository = deletableEntityRepository;
        }

        public IEnumerable<T> All<T>()
        {
            return this.userManager.Users
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.UserName)
                .To<T>()
                .ToArray();
        }

        public async Task<bool> UserExists(string id)
        {
            return await this.userManager.FindByIdAsync(id) != null;
        }

        public T GetUserById<T>(string id)
        {
            var user = this.userManager.Users
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
            return user;
        }

        public async Task EditUser(EditUserDto input)
        {
            var user = await this.userManager.FindByIdAsync(input.Id);

            user.AccessFailedCount = input.AccessFailedCount;
            user.Email = input.Email;
            user.EmailConfirmed = input.EmailConfirmed;
            user.LockoutEnabled = input.LockoutEnabled;
            user.LockoutEnd = input.LockoutEnd;
            user.PasswordHash = input.PasswordHash;
            user.PhoneNumber = input.PhoneNumber;
            user.PhoneNumberConfirmed = input.PhoneNumberConfirmed;
            user.TwoFactorEnabled = input.TwoFactorEnabled;
            user.UserName = input.UserName;

            await this.userManager.UpdateAsync(user);
        }

        public async Task<int> DeleteUserById(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            user.IsDeleted = true;
            user.Email = null;
            user.NormalizedEmail = null;
            user.UserName = null;
            user.NormalizedUserName = null;
            user.PhoneNumber = null;

            var result = await this.deletableEntityRepository.SaveChangesAsync();
            return result;
        }
    }
}
