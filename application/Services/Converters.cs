using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using Entities.Models;

namespace Application.Services
{
    public class Converters
    {
        public static async Task<Blob> FormFileToBlobAsync(IFormFile formfile)
        {
            using (var ms = new MemoryStream())
            {
                Blob blob = new Blob();

                await formfile.CopyToAsync(ms);
                blob.Buffer = ms.ToArray();
                blob.filename = formfile.FileName;
                blob.name = formfile.Name;
                blob.lenth = formfile.Length;
                return blob;
            }
        }

        public static IFormFile BlobToFormFile(Blob blob)
        {
            using (var ms = new MemoryStream(blob.Buffer))
            {
                var returnFormFile = new FormFile(ms, 0, blob.lenth, blob.name, blob.filename);

                return returnFormFile;
            }
        }
    }
}
