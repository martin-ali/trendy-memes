namespace TrendyMemes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using TrendyMemes.Data.Common.Models;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.PostTags = new HashSet<PostTag>();
            this.Votes = new HashSet<Vote>();
            this.Comments = new List<Comment>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Title { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Rating
        {
            get
            {
                return this.Votes.Sum(v => v.Value);
            }
        }

        [Required]
        public string ImageId { get; set; }

        public Image Image { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
