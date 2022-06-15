using System;
using System.IO;

namespace CookBook.Application.Services
{
    public class ImageService : IImageService
    {
        readonly string _folderName;

        public ImageService()
        {
            _folderName = Path.Combine( "Resources", "Images" );
        }

        public void SaveImage( Stream imageStream, string fileName )
        {
            string fileExtension = fileName.Split( "." )[ 1 ];
            string newFileName = $"{Guid.NewGuid().ToString()}.{fileExtension}";
            var newFilePath = Path.Combine( _folderName, newFileName );

            using ( FileStream fs = File.Create( newFilePath ) )
            {
                imageStream.CopyTo( fs );
            }

            // результат!
        }

    }
}
