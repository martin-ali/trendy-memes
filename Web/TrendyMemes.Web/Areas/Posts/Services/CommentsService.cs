namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System.Threading.Tasks;

    using TrendyMemes.Data.Common.Repositories;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Web.Areas.Posts.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task CreateAsync(CommentCreateInputModel input, string authorId)
        {
            var comment = new Comment
            {
                AuthorId = authorId,
                PostId = input.PostId,
                Content = input.Content,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }
    }
}
