namespace TrendyMemes.Web.Areas.Posts.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    using AutoMapper;

    using TrendyMemes.Common.Attributes;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.Mapping;

    public class PostEditInputModel : IMapFrom<Post>, IHaveCustomMappings
    {
        [Required]
        [MinLengthWithCustomMessage(3)]
        [MaxLengthWithCustomMessage(20)]
        public string Title { get; set; }

        [Required]
        [MinLengthWithCustomMessage(2)]
        [MaxLengthWithCustomMessage(200)]
        public string Tags { get; set; }

        public string ImageSrc { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, PostEditInputModel>()
                .ForMember(pe => pe.Tags, opt => opt.MapFrom(p => string.Join(' ', p.PostTags.Select(t => t.Tag.Name))))
                .ForMember(pe => pe.ImageSrc, opt => opt.MapFrom(p => Path.Combine(@"images", $"{p.ImageId}.{p.Image.Extension}")));
        }
    }
}
