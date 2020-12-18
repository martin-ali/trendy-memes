namespace TrendyMemes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TrendyMemes.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }
    }
}