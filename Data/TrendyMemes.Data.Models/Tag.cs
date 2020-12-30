namespace TrendyMemes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TrendyMemes.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.PostTags = new List<PostTag>();
        }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
