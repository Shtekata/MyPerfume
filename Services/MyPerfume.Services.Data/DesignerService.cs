namespace MyPerfume.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyPerfume.Data.Common.Repositories;
    using MyPerfume.Data.Models;
    using MyPerfume.Web.ViewModels.Dto;

    public class DesignerService : IDesignerService
    {
        private readonly IDeletableEntityRepository<Designer> deletableEntityRepository;

        public DesignerService(IDeletableEntityRepository<Designer> deletableEntityRepository)
        {
            this.deletableEntityRepository = deletableEntityRepository;
        }

        public async Task AddAsync(DesignerDto input)
        {
            var designer = new Designer { Name = input.Name };
            await this.deletableEntityRepository.AddAsync(designer);
            await this.deletableEntityRepository.SaveChangesAsync();
        }

        public async Task<Designer> GetByIdWithDeletedAsync(DesignerDto input)
        {
            var designer = await this.deletableEntityRepository.GetByIdWithDeletedAsync(input.Name);
            return designer;
        }

        public bool Exists(DesignerDto input)
        {
            var designer = this.deletableEntityRepository.AllAsNoTrackingWithDeleted()
                .FirstOrDefault(x => x.Name == input.Name);
            if (designer != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
