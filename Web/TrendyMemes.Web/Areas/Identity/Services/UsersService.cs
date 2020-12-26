namespace TrendyMemes.Web.Areas.Identity.Services
{
    using System.Linq;

    using TrendyMemes.Data.Common.Repositories;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.Mapping;
    using TrendyMemes.Web.Areas.Identity.ViewModels;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public UserDetailsViewModel GetProfileById(string id)
        {
            var user = this.usersRepository
                .AllAsNoTracking()
                .Where(u => u.Id == id)
                .To<UserDetailsViewModel>()
                .FirstOrDefault();

            return user;
        }
    }
}
