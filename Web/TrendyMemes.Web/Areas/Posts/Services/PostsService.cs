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
    using TrendyMemes.Web.Areas.Posts.ViewModels;
    using TrendyMemes.Web.Areas.Tags.Services;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;
        private readonly ITagsService tagsService;
        private readonly IFileWriter fileWriter;
        private readonly IFileValidator fileValidator;

        public PostsService(
            IDeletableEntityRepository<Post> postsRepository,
            ITagsService tagsService,
            IFileWriter fileWriter,
            IFileValidator fileValidator)
        {
            this.postsRepository = postsRepository;
            this.tagsService = tagsService;
            this.fileWriter = fileWriter;
            this.fileValidator = fileValidator;
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

        public IEnumerable<T> GetTopPercent<T>(double percentageToSkip, double percentageToTake)
        {
            var fractionTake = 100 / percentageToTake;
            var fractionSkip = 100 / percentageToSkip;
            var postsToSkip = (int)(this.postsRepository.All().Count() / fractionSkip);
            var postsToTake = (int)(this.postsRepository.All().Count() / fractionTake);

            var posts = this.postsRepository
                .AllAsNoTracking()
                .OrderByDescending(p => p.Votes.Sum(v => v.Value))
                .Skip(postsToSkip)
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

        public IEnumerable<T> GetByTagId<T>(int tagId)
        {
            var postsByTag = this.postsRepository
                .AllAsNoTracking()
                .Where(p => p.Tags.Any(t => t.TagId == tagId))
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

            foreach (var tagName in inputTags)
            {
                var tag = this.tagsService.GetByName(tagName);

                if (tag == null)
                {
                    tag = await this.tagsService.CreateTagAsync(tagName);
                }

                var postTag = new PostTag
                {
                    Post = post,
                    Tag = tag,
                };

                post.Tags.Add(postTag);
            }

            // I'm not 100% sure whether to leave the validation here or move it to the controller
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
            this.fileValidator.ThrowIfExtensionIsInvalid(extension);

            var image = new Image
            {
                UserAddedId = authorId,
                Extension = extension,
            };

            post.Image = image;
            await this.fileWriter.WriteImageFromHttp(input.Image, image.Id.ToString(), extension);

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();

            return post.Id;
        }
    }
}
