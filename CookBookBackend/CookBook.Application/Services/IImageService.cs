using System.IO;

namespace CookBook.Application.Services
{
    public interface IImageService
    {
        void SaveImage( Stream imageStream, string fileName );
    }
}