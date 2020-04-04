namespace MyPerfume.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MyPerfume.Data.Common.Models;
    using MyPerfume.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Perfume> Perfumes { get; set; }

        public DbSet<Designer> Designers { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<PerfumeSeason> PerfumesSeasons { get; set; }

        public DbSet<PerfumePurpose> PerfumesPurposes { get; set; }

        public DbSet<Perfumer> Perfumers { get; set; }

        public DbSet<PerfumePerfumer> PerfumesPerfumers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<PerfumeCategorie> PerfumesCategories { get; set; }

        public DbSet<TopNote> TopNotes { get; set; }

        public DbSet<PerfumeTopNote> PerfumesTopNotes { get; set; }

        public DbSet<HeartNote> HeartNotes { get; set; }

        public DbSet<PerfumeHeartNote> PerfumesHeartNotes { get; set; }

        public DbSet<BaseNote> BaseNotes { get; set; }

        public DbSet<PerfumeBaseNote> PerfumesBaseNotes { get; set; }

        public DbSet<AromaticGroup> AromaticGroups { get; set; }

        public DbSet<PerfumeAromaticGroup> PerfumesAromaticGroups { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            builder.Entity<Perfume>()
                .Property(x => x.CustomerType).HasConversion<string>();

            builder.Entity<PerfumeSeason>(x =>
            {
                x.HasKey(x => new { x.PerfumeId, x.SeasonId });
                x.Property(x => x.SeasonId).HasConversion<string>();
            });

            builder.Entity<PerfumePurpose>(x =>
            {
                x.HasKey(x => new { x.PerfumeId, x.PurposeId });
                x.Property(x => x.PurposeId).HasConversion<string>();
            });

            builder.Entity<PerfumePerfumer>(x =>
            {
                x.HasKey(x => new { x.PerfumeId, x.PerfumerId });
            });

            builder.Entity<PerfumeCategorie>(x =>
            {
                x.HasKey(x => new { x.PerfumeId, x.CategoryId });
            });

            builder.Entity<PerfumeTopNote>(x =>
            {
                x.HasKey(x => new { x.PerfumeId, x.TopNoteId });
            });

            builder.Entity<PerfumeHeartNote>(x =>
            {
                x.HasKey(x => new { x.PerfumeId, x.HeartNoteId });
            });

            builder.Entity<PerfumeBaseNote>(x =>
            {
                x.HasKey(x => new { x.PerfumeId, x.BaseNoteId });
            });

            builder.Entity<PerfumeAromaticGroup>(x =>
            {
                x.HasKey(x => new { x.PerfumeId, x.AromaticGroupId });
            });

            builder.Entity<Product>()
                .Property(x => x.PerfumeType).HasConversion<string>();

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
