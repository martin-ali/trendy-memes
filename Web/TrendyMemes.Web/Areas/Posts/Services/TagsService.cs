namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrendyMemes.Data.Common.Repositories;
    using TrendyMemes.Data.Models;

    public class TagsService : ITagsService
    {
        private readonly IDeletableEntityRepository<Tag> tagsRepository;

        public TagsService(IDeletableEntityRepository<Tag> tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        public IEnumerable<Tag> GetAll()
        {
            var tags = this.tagsRepository
                .AllAsNoTracking();

            return tags;
        }

        public Tag GetById(int id)
        {
            var tag = this.tagsRepository
                .AllAsNoTracking()
                .FirstOrDefault(t => t.Id == id);

            return tag;
        }

        public Tag GetByName(string name)
        {
            var tag = this.tagsRepository
                .AllAsNoTracking()
                .FirstOrDefault(t => t.Name == name);

            // FIXME: String comparison
            // String comparison issue. In the above line, comparison is case insensitive for some reason. I don't know how to fix it, so I've added this WTF segment of code
            if (tag?.Name != name)
            {
                return null;
            }

            return tag;
        }

        public async Task<Tag> CreateTagAsync(string name)
        {
            var tag = new Tag { Name = name };

            await this.tagsRepository.AddAsync(tag);


        public async Task<Tag> GuaranteeTagAsync(string name)
        {
            var tag = this.tagsRepository.AllAsNoTracking()
                .FirstOrDefault(t => t.Name == name);

            if (tag == null)
            {
                tag = await this.CreateTagAsync(name);
            }

            return tag;
        }

        public void Delete(int tagId)
        {
            var tag = this.tagsRepository.All()
                .FirstOrDefault(t => t.Id == tagId);

            this.tagsRepository.Delete(tag);
        }
    }
}
