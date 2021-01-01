namespace TrendyMemes.Web.Areas.Posts.ViewModels.Posts
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.Configuration.Annotations;

    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.Mapping;
    using TrendyMemes.Web.Areas.Tags.ViewModels;

    public class PostInListViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public PostInListViewModel()
        {
            this.Tags = new List<TagInListViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public string ImagePath { get; set; }

        public string ImageExtension { get; set; }

        [SourceMember(nameof(Post.Rating))]
        public int Rating { get; set; }

        // [SourceMember(nameof(Post.PostTags))]
        public IEnumerable<TagInListViewModel> Tags { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            // NOTE: This is a hack for an issue I could not solve. The code sucks and I am ashamed of it, but time is of the essence
            configuration
            .CreateMap<Post, PostInListViewModel>()
            .Include<Post, PostDetailsViewModel>()
            .ForMember(vm => vm.ImagePath, opt => opt.MapFrom(p => Path.Combine(@"images", $"{p.ImageId}.{p.Image.Extension}")))
            .ForMember(vm => vm.Tags, opt => opt.MapFrom(p => p.PostTags.Select(pt => pt.Tag)));
        }
    }
}
