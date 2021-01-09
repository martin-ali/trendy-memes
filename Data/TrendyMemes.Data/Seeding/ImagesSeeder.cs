namespace TrendyMemes.Data.Seeding
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;

    using TrendyMemes.Common;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.IO;

    public class ImagesSeeder : ISeeder
    {
        // FIXME:  This is a magic string
        private const string ImagesSeedingRelativeDirectory = @"Data\TrendyMemes.Data\Seeding\Images";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Images.Any())
            {
                return;
            }

            // Get all images for seeding
            var environment = (IWebHostEnvironment)serviceProvider.GetService(typeof(IWebHostEnvironment));
            var root = Path.GetFullPath(Path.Combine(environment.ContentRootPath, @"..\.."));
            var imagesDirectory = Path.Combine(root, ImagesSeedingRelativeDirectory);
            var filePaths = Directory.GetFiles(imagesDirectory);

            var users = dbContext.Users.ToList();
            var random = new Random();

            var fileWriter = (IImageOperator)serviceProvider.GetService(typeof(IImageOperator));

            var filesCount = filePaths.Count();
            for (int i = 0; i < filesCount; i++)
            {
                var seedPath = filePaths[i];
                var extension = Path
                    .GetExtension(seedPath)
                    .TrimStart('.');

                var author = users[random.Next(0, users.Count)];
                var image = new Image
                {
                    UserAdded = author,
                    Extension = extension,
                };

                var bytes = await File.ReadAllBytesAsync(seedPath);
                await fileWriter.Write(bytes, image.Id, extension);

                await dbContext.Images.AddAsync(image);
            }
        }
    }
}
