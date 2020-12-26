namespace TrendyMemes.Services.IO
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    using TrendyMemes.Common;

    public class FileWriter : IFileWriter
    {
        private readonly IWebHostEnvironment environment;

        private readonly string imagesPath;

        public FileWriter(IWebHostEnvironment environment)
        {
            this.environment = environment;
            this.imagesPath = Path.Combine(this.environment.WebRootPath, GlobalConstants.ImagesDirectory);

            Directory.CreateDirectory(this.imagesPath);
        }

        public async Task WriteImageFromHttp(IFormFile file, string fileName, string extension)
        {
            var fileNameAndExtension = $"{fileName}.{extension}";

            var path = Path.Combine(this.imagesPath, fileNameAndExtension);
            using Stream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }

        public async Task WriteImageFromBytes(byte[] image, string name)
        {
            var path = Path.Combine(this.imagesPath, name);
            await File.WriteAllBytesAsync(path, image);
        }
    }
}
