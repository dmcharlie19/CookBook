using System.IO;

namespace CookBook.Application.Services
{
    public interface IImageService
    {
        FileInfo LoadImage( string path );
        string SaveImage( Stream imageStream, string fileName );
    }
}