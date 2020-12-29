namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TrendyMemes.Data.Models;

    public interface ITagsService
    {
        IEnumerable<Tag> GetAll();

        Tag GetByName(string name);

        Tag GetById(int id);

        Task<Tag> CreateTagAsync(string name);

        Task<Tag> GuaranteeTagAsync(string name);

        void Delete(int tagId);
    }
}
