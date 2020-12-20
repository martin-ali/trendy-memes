namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using TrendyMemes.Data.Common.Repositories;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.Mapping;
    using TrendyMemes.Web.Areas.Posts.Viewmodels;

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
                .OrderByDescending(p => p.Votes.Sum(v => v.Value))
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
                .OrderByDescending(p => p.Votes.Sum(v => v.Value))
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

        public void Create(CreatePostInputModel input)
        {
            throw new System.NotImplementedException();
        }
    }
}
