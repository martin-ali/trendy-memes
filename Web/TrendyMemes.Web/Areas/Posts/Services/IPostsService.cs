namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System.Collections.Generic;

    using TrendyMemes.Web.Areas.Posts.Viewmodels;

    public interface IPostsService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetTopPercent<T>(double percentageToSkip, double percentageToTake);

        IEnumerable<T> GetByTag<T>(int tagId);

        T GetById<T>(int id);

        void Create(CreatePostInputModel input);
    }
}
