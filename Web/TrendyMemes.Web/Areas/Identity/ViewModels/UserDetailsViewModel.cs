namespace TrendyMemes.Web.Areas.Identity.ViewModels
{
    using System.Collections.Generic;

    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.Mapping;

    public class UserDetailsViewModel : IMapFrom<ApplicationUser>
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
