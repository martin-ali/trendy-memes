namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task VoteOnPostAsync(int postId, string userId, int value);
    }
}
