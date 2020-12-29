namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TrendyMemes.Web.Areas.Posts.ViewModels.Posts;

    public interface IPostsService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetTopPercent<T>(double percentageToSkip, double percentageToTake);

        IEnumerable<T> GetByTagId<T>(int tagId);

        T GetById<T>(int id);

        Task<int> CreateAsync(PostCreateInputModel input, string authorId, IEnumerable<string> inputTags);

        IEnumerable<T> GetByUserId<T>(string userId);
    }
}
