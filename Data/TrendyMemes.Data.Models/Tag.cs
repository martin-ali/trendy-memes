namespace TrendyMemes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TrendyMemes.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.Posts = new List<Post>();
        }

        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
