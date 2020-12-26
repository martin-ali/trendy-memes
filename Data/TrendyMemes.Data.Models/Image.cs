namespace TrendyMemes.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TrendyMemes.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Extension { get; set; }

        [Required]
        public string UserAddedId { get; set; }

        public ApplicationUser UserAdded { get; set; }
    }
}
