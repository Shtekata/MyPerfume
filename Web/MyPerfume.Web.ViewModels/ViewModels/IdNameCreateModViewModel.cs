namespace MyPerfume.Web.ViewModels.ViewModels
{
    using System;

    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class IdNameCreateModViewModel : IMapFrom<Designer>, IMapFrom<Country>, IMapFrom<Color>, IMapFrom<AromaticGroup>, IMapFrom<BaseNote>, IMapFrom<Category>, IMapFrom<HeartNote>, IMapFrom<Perfumer>, IMapFrom<TopNote>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
