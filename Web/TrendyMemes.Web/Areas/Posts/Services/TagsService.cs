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
            var tags = this.tagsRepository.AllAsNoTracking();

            return tags;
        }

        public Tag GetById(int id)
        {
            var tag = this.tagsRepository.AllAsNoTracking()
                .FirstOrDefault(t => t.Id == id);

            return tag;
        }

        public Tag GetByName(string name)
        {
            // Apparently tags are case insensitive in this project. I have no idea how to fix this
            // NOTE: Wrong collation?
            var tag = this.tagsRepository.AllAsNoTracking()
                .FirstOrDefault(t => t.Name == name);

            return tag;
        }

        public async Task<Tag> CreateTagAsync(string name)
        {
            var tag = new Tag { Name = name };

            await this.tagsRepository.AddAsync(tag);
            await this.tagsRepository.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag> GuaranteeTagExistsAsync(string name)
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
