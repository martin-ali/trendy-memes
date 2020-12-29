namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System.Threading.Tasks;

    using TrendyMemes.Web.Areas.Posts.ViewModels.Comments;

    public interface ICommentsService
    {
        Task CreateAsync(CommentCreateInputModel input, string authorId);
    }
}
