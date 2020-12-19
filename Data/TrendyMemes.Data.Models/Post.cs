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
        public int ImageId { get; set; }

        public Image Image { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Title { get; set; }

        public ApplicationUser Author { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<Vote> Votes { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
