namespace MyPerfume.Web.ViewModels.Dtos
{
    using System;
    using System.Collections.Generic;

    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class PostDto : IMapFrom<Post>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent => this.Content?.Length > 300 ? this.Content?.Substring(0, 300) + "..." : this.Content;

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }

        // public IEnumerable<CommentDto> Comments { get; set; }
    }
}
