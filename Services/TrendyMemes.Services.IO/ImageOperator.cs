namespace TrendyMemes.Services.IO
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    using TrendyMemes.Common;

    public class ImageOperator : IFileOperator
    {
        private readonly IWebHostEnvironment environment;

        private readonly string imagesPath;

        public ImageOperator(IWebHostEnvironment environment)
        {
            this.environment = environment;
            this.imagesPath = Path.Combine(this.environment.WebRootPath, GlobalConstants.ImagesDirectory);

            Directory.CreateDirectory(this.imagesPath);
        }

        public async Task Write(IFormFile file, string fileName, string extension)
        {
            var fileNameAndExtension = $"{fileName}.{extension}";

            var path = Path.Combine(this.imagesPath, fileNameAndExtension);
            using Stream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }

        public async Task Write(byte[] image, string name, string extension)
        {
            var path = Path.Combine(this.imagesPath, $"{name}.{extension}");
            await File.WriteAllBytesAsync(path, image);
        }

        public void Delete(string id, string extension)
        {
            var fileName = $"{id}.{extension}";
            var path = Path.Combine(this.imagesPath, fileName);

            File.Delete(path);
        }
    }
}
