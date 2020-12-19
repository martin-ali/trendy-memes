namespace TrendyMemes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using TrendyMemes.Data.Common.Repositories;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.Mapping;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }
        public IEnumerable<T> GetAll<T>()
        {
            var posts = this.postsRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();

            return posts;
        }

        public IEnumerable<T> GetTopPercent<T>(double percentage)
        {
            var fraction = 100 / percentage;
            var postsToTake = (int)(this.postsRepository.All().Count() / fraction);

            var posts = this.postsRepository
                .AllAsNoTracking()
                .OrderByDescending(p => p.Votes.Sum(v => v.IsUpvote ? 1 : -1))
                .Take(postsToTake)
                .To<T>()
                .ToList();

            return posts;
        }

        public T GetById<T>(int id)
        {
            var post = this.postsRepository
                .AllAsNoTracking()
                .Where(p => p.Id == id)
                .To<T>()
                .FirstOrDefault();

            return post;
        }

        public IEnumerable<T> GetByTag<T>(int tagId)
        {
            var postsByTag = this.postsRepository
                .AllAsNoTracking()
                .Where(p => p.Tags.Any(t => t.Id == tagId))
                .To<T>()
                .ToList();

            return postsByTag;
        }
    }
}
