namespace TrendyMemes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TrendyMemes.Data.Common.Models;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.Tags = new List<Tag>();
            this.Votes = new List<Vote>();
            this.Comments = new List<Comment>();
        }

        [Required]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<Vote> Votes { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
