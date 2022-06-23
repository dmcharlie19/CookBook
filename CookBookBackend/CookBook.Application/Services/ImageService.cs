using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CookBook.Application.Services
{
    public class ImageService : IImageService
    {
        readonly string _folder;

        public ImageService( IConfiguration configRoot )
        {
            _folder = configRoot[ "ImagesFolderPath" ];
        }

        public string SaveImage( Stream imageStream, string fileName )
        {
            if ( _folder == "" )
                throw new ArgumentException( "Неопределен путь для сохранения изображений" );

            Directory.CreateDirectory( _folder );

            string fileExtension = fileName.Split( "." )[ 1 ];
            string newFileName = $"{Guid.NewGuid().ToString()}.{fileExtension}";
            var newFilePath = Path.Combine( _folder, newFileName );

            using ( FileStream fs = File.Create( newFilePath ) )
            {
                imageStream.CopyTo( fs );
            }

            return newFilePath;
        }
    }
}
