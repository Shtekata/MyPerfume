namespace MyPerfume.Web.ViewModels.Dtos
{
    using MyPerfume.Data.Models;
    using MyPerfume.Services.Mapping;

    public class CommentDto : IMapFrom<Comment>
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public string PostId { get; set; }
    }
}
