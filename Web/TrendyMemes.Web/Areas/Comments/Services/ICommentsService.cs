namespace TrendyMemes.Web.Areas.Comments.Services
{
    using System.Threading.Tasks;

    using TrendyMemes.Web.Areas.Comments.ViewModels;

    public interface ICommentsService
    {
        Task CreateAsync(CommentCreateInputModel input, string authorId);
    }
}
