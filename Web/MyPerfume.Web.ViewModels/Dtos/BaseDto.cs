namespace MyPerfume.Web.ViewModels.Dtos
{
    using System;

    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.InputModels;
    using MyPerfume.Web.ViewModels.ViewModels;

    public class BaseDto : IMapFrom<BaseInputModel>, IMapTo<BaseInputModel>, IMapTo<BaseViewModel>, IMapFrom<Designer>, IMapFrom<AromaticGroup>, IMapFrom<BaseNote>, IMapFrom<Category>, IMapFrom<Color>, IMapFrom<Country>, IMapFrom<HeartNote>, IMapFrom<Perfumer>, IMapFrom<TopNote>
    {
        public BaseDto()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
