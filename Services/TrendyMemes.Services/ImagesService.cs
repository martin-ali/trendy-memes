namespace TrendyMemes.Services
{
    using System.IO;

    using Microsoft.AspNetCore.Hosting;

    using TrendyMemes.Common;

    public class ImagesService : IImagesService
    {
        private readonly string imagesPath;
        private readonly IWebHostEnvironment environment;

        public ImagesService(IWebHostEnvironment environment)
        {
            this.environment = environment;

            this.imagesPath = Path.Combine(this.environment.WebRootPath, GlobalConstants.ImagesDirectory);
        }

        public string GetImagePathByIdAsync(string id)
        {
            return Path.Combine(this.imagesPath, id);
        }
    }
}
