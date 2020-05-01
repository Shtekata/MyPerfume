namespace MyPerfume.Web.ViewModels.Dtos
{
    using System;

    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;
    using MyPerfume.Web.ViewModels.InputModels;

    public class PostDto : IMapFrom<Post>, IMapTo<Post>, IMapFrom<PostInputModel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent => this.Content?.Length > 300 ? this.Content?.Substring(0, 300) + "..." : this.Content;

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string PerfumeId { get; set; }

        public string PerfumeName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }

        // public IEnumerable<CommentDto> Comments { get; set; }
    }
}
