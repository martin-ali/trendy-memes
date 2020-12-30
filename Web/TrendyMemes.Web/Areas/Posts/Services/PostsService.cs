namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrendyMemes.Data.Common.Repositories;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.Mapping;
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
                .Where(p => p.PostTags.Any(t => t.TagId == tagId))
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

                post.PostTags.Add(postTag);
            }

            var image = await this.imagesService.CreateImage(input.Image, authorId);
            post.Image = image;

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();

            return post.Id;
        }

        public async Task UpdateAsync(PostEditInputModel input, int postId, IEnumerable<string> inputTags)
        {
            var post = this.postsRepository
                .All()
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .Where(p => p.Id == postId)
                .FirstOrDefault();

            // Update title
            post.Title = input.Title;

            // Add new tags
            foreach (var inputTag in inputTags)
            {
                await this.GuaranteePostHasTag(post, inputTag);
            }

            // Delete removed tags
            foreach (var postTag in post.PostTags)
            {
                if (inputTags.Contains(postTag.Tag.Name))
                {
                    continue;
                }

                this.postTagsRepository.Delete(postTag);
            }

            // this.postsRepository.Update(post);
            await this.postsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int postId)
        {
            var post = this.postsRepository.All()
                .FirstOrDefault(p => p.Id == postId);

            this.postsRepository.Delete(post);
            await this.imagesService.DeleteImage(post.ImageId);

            await this.postsRepository.SaveChangesAsync();
        }

        private void GuaranteePostHasTag(Post post, Tag tag)
        {
            var postTag = post.PostTags.FirstOrDefault(pt => pt.TagId == tag.Id);

            if (postTag == null)
            {
                postTag = new PostTag
                {
                    Post = post,
                    TagId = tag.Id,
                };

                post.PostTags.Add(postTag);
            }
        }

        private async Task GuaranteePostHasTag(Post post, string tagName)
        {
            var postTag = this.postTagsRepository.AllAsNoTracking()
                .FirstOrDefault(pt => pt.Tag.Name == tagName);

            if (postTag == null)
            {
                var tag = await this.tagsService.GuaranteeTagAsync(tagName);

                postTag = new PostTag
                {
                    Post = post,
                    TagId = tag.Id,
                };

                post.PostTags.Add(postTag);
            }
        }
    }
}
