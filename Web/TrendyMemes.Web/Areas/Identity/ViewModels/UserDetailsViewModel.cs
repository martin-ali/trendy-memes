namespace TrendyMemes.Web.Areas.Identity.ViewModels
{
    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.Mapping;

    public class UserDetailsViewModel : IMapFrom<ApplicationUser>
    {
        public string Username { get; set; }
    }
}
