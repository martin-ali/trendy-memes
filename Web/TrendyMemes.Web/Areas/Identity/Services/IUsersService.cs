namespace TrendyMemes.Web.Areas.Identity.Services
{
    using TrendyMemes.Web.Areas.Identity.ViewModels;

    public interface IUsersService
    {
        UserDetailsViewModel GetProfileById(string id);
    }
}
