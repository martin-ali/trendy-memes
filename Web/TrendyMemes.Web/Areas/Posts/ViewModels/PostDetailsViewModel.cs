namespace TrendyMemes.Web.Areas.Posts.ViewModels
{
    using System.Collections.Generic;

    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.Mapping;
    using TrendyMemes.Web.ViewModels.Comments;
    using TrendyMemes.Web.ViewModels.Tags;

    public class PostDetailsViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public string ImagePath { get; set; }

        public IEnumerable<TagInListViewModel> Tags { get; set; }

        public IEnumerable<CommentListViewModel> Comments { get; set; }
    }
}
