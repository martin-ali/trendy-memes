namespace TrendyMemes.Web.Areas.Tags.Services
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
    }
}
