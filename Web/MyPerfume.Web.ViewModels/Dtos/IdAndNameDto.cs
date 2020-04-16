﻿namespace MyPerfume.Web.ViewModels.Dtos
{
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class IdAndNameDto : IMapFrom<Designer>, IMapFrom<Country>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}