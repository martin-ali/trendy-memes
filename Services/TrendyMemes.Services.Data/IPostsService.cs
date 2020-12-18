namespace TrendyMemes.Services.Data
{
    using System.Collections.Generic;

    using TrendyMemes.Web.ViewModels.Posts;

    public interface IPostsService
    {
        IEnumerable<PostInListViewModel> GetAllPosts();

        IEnumerable<PostInListViewModel> GetPostsByTag(int tagId);

        PostDetailsViewModel GetPostById(int id);
    }
}
