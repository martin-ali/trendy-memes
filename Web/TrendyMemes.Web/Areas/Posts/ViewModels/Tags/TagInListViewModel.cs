namespace TrendyMemes.Web.Areas.Tags.ViewModels
{
    using AutoMapper;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.Mapping;

    public class TagInListViewModel : IMapFrom<Tag>, IMapFrom<PostTag>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<PostTag, TagInListViewModel>()
                .ForMember(vm => vm.Id, opt => opt.MapFrom(pt => pt.TagId))
                .ForMember(vm => vm.Name, opt => opt.MapFrom(pt => pt.Tag.Name));
        }
    }
}
