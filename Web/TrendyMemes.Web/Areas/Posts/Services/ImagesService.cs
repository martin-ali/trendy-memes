namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System;
    using System.IO;
    using System.Linq;
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
        private readonly IFileOperator fileOperator;
        private readonly IFileValidator fileValidator;

        public ImagesService(IDeletableEntityRepository<Image> imagesRepository, IFileOperator fileOperator, IFileValidator fileValidator)
        {
            this.imagesRepository = imagesRepository;
            this.fileOperator = fileOperator;
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

            await this.fileOperator.Write(input, image.Id.ToString(), extension);
            await this.imagesRepository.AddAsync(image);

            await this.imagesRepository.SaveChangesAsync();

            return image;
        }

        public async Task DeleteImage(string imageId)
        {
            var image = this.imagesRepository.All()
                .FirstOrDefault(i => i.Id == imageId);

            this.imagesRepository.Delete(image);
            await this.imagesRepository.SaveChangesAsync();
        }

        public string GetRelativeImagePath(string imageId)
        {
            string fileName = this.GetFileName(imageId);
            var path = Path.Combine(GlobalConstants.ImagesDirectory, fileName);

            return path;
        }

        private string GetFileName(string imageId)
        {
            var extension = this.imagesRepository.All()
                .Where(i => i.Id == imageId)
                .Select(i => $"{i.Id}.{i.Extension}")
                .FirstOrDefault();

            return extension;
        }
    }
}
