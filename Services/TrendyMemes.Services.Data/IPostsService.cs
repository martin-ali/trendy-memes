namespace TrendyMemes.Services.Data
{
    using System.Collections.Generic;

    public interface IPostsService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetTopPercent<T>(double percentage);

        IEnumerable<T> GetByTag<T>(int tagId);

        T GetById<T>(int id);
    }
}
