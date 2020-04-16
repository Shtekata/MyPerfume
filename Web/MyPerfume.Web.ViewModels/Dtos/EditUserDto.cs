namespace MyPerfume.Web.ViewModels.Dtos
{
    using System;

    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.Administration.Users;

    public class EditUserDto : IMapFrom<EditUserViewModel>
    {
        public virtual DateTimeOffset? LockoutEnd { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string ConcurrencyStamp { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual string NormalizedEmail { get; set; }

        public virtual string Email { get; set; }

        public virtual string NormalizedUserName { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Id { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
