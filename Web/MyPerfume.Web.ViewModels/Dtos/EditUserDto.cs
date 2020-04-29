namespace MyPerfume.Web.ViewModels.Dtos
{
    using System;

    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Administration.Users;

    public class EditUserDto : IMapFrom<EditUserViewModel>
    {
        public DateTimeOffset? LockoutEnd { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string SecurityStamp { get; set; }

        public string PasswordHash { get; set; }

        public bool EmailConfirmed { get; set; }

        public string NormalizedEmail { get; set; }

        public string Email { get; set; }

        public string NormalizedUserName { get; set; }

        public string UserName { get; set; }

        public string Id { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
