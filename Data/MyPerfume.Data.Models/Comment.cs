namespace MyPerfume.Data.Models
{
    using MyPerfume.Data.Common.Models;

    public class Comment : BaseDeletableModel<string>
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
