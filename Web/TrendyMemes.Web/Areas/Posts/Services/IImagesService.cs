namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using TrendyMemes.Data.Models;

    public interface IImagesService
    {
        Task<Image> CreateImage(IFormFile input, string authorId);

        Task DeleteImage(string imageId);

        string GetRelativeImagePath(string imageId);
    }
}
