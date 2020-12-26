namespace TrendyMemes.Services.IO
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IFileWriter
    {
        Task WriteImageFromHttp(IFormFile file, string fileName, string extension);

        Task WriteImageFromBytes(byte[] image, string name);
    }
}
