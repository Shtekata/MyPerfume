namespace MyPerfume.Data.Models
{
    using System.Collections.Generic;

    using MyPerfume.Data.Common.Models;

    public class Post : BaseDeletableModel<string>
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string PerfumeId { get; set; }

        public virtual Perfume Perfume { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
