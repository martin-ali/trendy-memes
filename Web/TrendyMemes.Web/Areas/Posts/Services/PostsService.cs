namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using TrendyMemes.Data.Common.Repositories;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.IO;
    using TrendyMemes.Services.Mapping;
    using TrendyMemes.Services.Validation;
    using TrendyMemes.Web.Areas.Posts.ViewModels.Posts;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;
        private readonly IDeletableEntityRepository<PostTag> postTagsRepository;
        private readonly ITagsService tagsService;
        private readonly IImagesService imagesService;

        public PostsService(
            IDeletableEntityRepository<Post> postsRepository,
            IDeletableEntityRepository<PostTag> postTagsRepository,
            ITagsService tagsService,
            IImagesService imagesService)
        {
            this.postsRepository = postsRepository;
            this.postTagsRepository = postTagsRepository;
            this.tagsService = tagsService;
            this.imagesService = imagesService;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.GetTopPercent<T>(0, 100);
        }

        public IEnumerable<T> GetTopPercent<T>(double percentageToSkip, double percentageToTake)
        {
            var fractionSkip = 100 / percentageToSkip;
            var fractionTake = 100 / percentageToTake;

            var postsToSkip = (int)(this.postsRepository.All().Count() / fractionSkip);
            var postsToTake = (int)(this.postsRepository.All().Count() / fractionTake);

            var posts = this.postsRepository
                .AllAsNoTracking()
                .OrderByDescending(p => p.Votes.Sum(v => v.Value))
                .Skip(postsToSkip)
                .Take(postsToTake)
                .OrderByDescending(p => p.CreatedOn)
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

        public IEnumerable<T> GetByTagId<T>(int tagId)
        {
            var postsByTag = this.postsRepository
                .AllAsNoTracking()
                .Where(p => p.Tags.Any(t => t.TagId == tagId))
                .To<T>()
                .ToList();

            return postsByTag;
        }

        public IEnumerable<T> GetByUserId<T>(string userId)
        {
            var postsByTag = this.postsRepository
                .AllAsNoTracking()
                .Where(p => p.AuthorId == userId)
                .To<T>()
                .ToList();

            return postsByTag;
        }

        public async Task<int> CreateAsync(PostCreateInputModel input, string authorId, IEnumerable<string> inputTags)
        {
            var post = new Post
            {
                Title = input.Title,
                AuthorId = authorId,
            };

            foreach (var name in inputTags)
            {
                var tag = await this.tagsService.GuaranteeTagAsync(name);

                var postTag = new PostTag
                {
                    Post = post,
                    TagId = tag.Id,
                };

                post.Tags.Add(postTag);
            }

            var image = await this.imagesService.CreateImage(input.Image, authorId);
            post.Image = image;

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();

            return post.Id;
        }

        public async Task<int> UpdateAsync(PostEditInputModel input, int postId, IEnumerable<string> inputTags)
        {
            var post = this.postsRepository.All()
                .FirstOrDefault(p => p.Id == postId);

            // Update title
            post.Title = input.Title;

            foreach (var name in inputTags)
            {
                if (post.Tags.Any(pt => pt.Tag.Name == name))
                {
                    continue;
                }

                var tag = await this.tagsService.GuaranteeTagAsync(name);
                this.GuaranteePostTag(post, tag);
            }

            await this.postsRepository.SaveChangesAsync();

            return post.Id;
        }

        public async Task DeleteAsync(int postId)
        {
            var post = this.postsRepository.All()
                .FirstOrDefault(p => p.Id == postId);

            this.postsRepository.Delete(post);

            await this.postsRepository.SaveChangesAsync();
        }

        private void GuaranteePostTag(Post post, Tag tag)
        {
            var postTag = post.Tags.FirstOrDefault(pt => pt.TagId == tag.Id);

            if (postTag == null)
            {
                postTag = new PostTag
                {
                    Post = post,
                    TagId = tag.Id,
                };

                post.Tags.Add(postTag);
            }
        }
    }
}
