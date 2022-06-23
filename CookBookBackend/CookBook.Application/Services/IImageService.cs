using System.IO;

namespace CookBook.Application.Services
{
    public interface IImageService
    {
        string SaveImage( Stream imageStream, string fileName );
    }
}