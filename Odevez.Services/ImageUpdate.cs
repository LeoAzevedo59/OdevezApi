using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Odevez.Services
{
    public class ImageUpdate
    {
        public string UploadBase64Image(string base64Image, string container)
        {
            string connection = "DefaultEndpointsProtocol=https;AccountName=containerodevez;AccountKey=LIpA085lUwMWDIQ/p+w4ttw6AdRShxubIUHT7Zp37wC4LgpqP2nwst6NCaB1RH8/3qDpSDYRvBts+ASt4sDU9g==;EndpointSuffix=core.windows.net";

            var fileName = $"{Guid.NewGuid()}.jpg";

            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

            byte[] imageBytes = Convert.FromBase64String(data);

            var blobClient = new BlobClient(connection, container, fileName);

            using ( var stream = new MemoryStream(imageBytes))
            {
                blobClient.Upload(stream);
            }

            return fileName;
        }
    }
}
