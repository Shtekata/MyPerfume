namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Identity;
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public IEnumerable<T> All<T>()
        {
            return this.userManager.Users
                .To<T>()
                .ToArray();
        }
    }
}
