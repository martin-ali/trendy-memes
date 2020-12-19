namespace TrendyMemes.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    using TrendyMemes.Web.ViewModels.Tags;

    public class PostInListViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public IEnumerable<TagInListViewModel> Tags { get; set; }
    }
}
