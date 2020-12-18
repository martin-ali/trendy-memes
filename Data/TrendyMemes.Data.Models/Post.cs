namespace TrendyMemes.Data.Models
{
    using System.Collections.Generic;

    using TrendyMemes.Data.Common.Models;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.Tags = new List<Tag>();
            this.Votes = new List<Vote>();
            this.Comments = new List<Comment>();
        }

        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<Vote> Votes { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
