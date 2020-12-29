namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using TrendyMemes.Common;
    using TrendyMemes.Data.Common.Repositories;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.IO;
    using TrendyMemes.Services.Validation;

    public class ImagesService : IImagesService
    {
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly IFileWriter fileWriter;
        private readonly IFileValidator fileValidator;

        public ImagesService(IDeletableEntityRepository<Image> imagesRepository, IFileWriter fileWriter, IFileValidator fileValidator)
        {
            this.imagesRepository = imagesRepository;
            this.fileWriter = fileWriter;
            this.fileValidator = fileValidator;
        }

        public async Task<Image> CreateImage(IFormFile input, string authorId)
        {
            var extension = Path.GetExtension(input.FileName).TrimStart('.');
            this.fileValidator.ThrowIfExtensionIsInvalid(extension);

            var image = new Image
            {
                UserAddedId = authorId,
                Extension = extension,
            };

            await this.fileWriter.WriteImageFromHttp(input, image.Id.ToString(), extension);
            await this.imagesRepository.AddAsync(image);

            await this.imagesRepository.SaveChangesAsync();

            return image;
        }

        public string GetImageSrcById(string id)
        {
            var path = Path.Combine(GlobalConstants.ImagesDirectory, id);

            return path;
        }
    }
}
