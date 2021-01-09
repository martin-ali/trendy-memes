namespace TrendyMemes.Services.IO
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IImageOperator
    {
        Task Write(IFormFile file, string fileName, string extension);

        Task Write(byte[] image, string name, string extension);

        void Delete(string id, string extension);
    }
}
