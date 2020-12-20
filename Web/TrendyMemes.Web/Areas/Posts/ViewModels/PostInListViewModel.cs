namespace TrendyMemes.Web.Areas.Posts.Viewmodels
{
    using System.Collections.Generic;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.Mapping;
    using TrendyMemes.Web.ViewModels.Tags;

    public class PostInListViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public string ImagePath { get; set; }

        public int Score { get; set; }

        public IEnumerable<TagInListViewModel> Tags { get; set; }
    }
}
