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
                if (formfile == null) return null;

                Blob blob = new Blob();

                await formfile.CopyToAsync(ms);
                blob.Buffer = ms.ToArray();
                blob.filename = formfile.FileName;
                blob.name = formfile.Name;
                blob.lenth = formfile.Length;
                return blob;
            }
        }

        public static Blob FormFileToBlob(IFormFile formfile)
        {
            using (var ms = new MemoryStream())
            {
                if (formfile == null) return null;

                Blob blob = new Blob();

                formfile.CopyTo(ms);
                blob.Buffer = ms.ToArray();
                blob.filename = formfile.FileName;
                blob.name = formfile.Name;
                blob.lenth = formfile.Length;
                return blob;
            }
        }

        public static async Task<IEnumerable<Blob>> FormFilesToBlobsAsync(IEnumerable<IFormFile> formfiles)
        {
            List<Blob> blobs = new List<Blob>();
            foreach (IFormFile ff in formfiles)
                if (ff != null)
                {
                    blobs.Add(await FormFileToBlobAsync(ff));
                }
            return blobs;
        }

        public static IEnumerable<Blob> FormFilesToBlobs(IEnumerable<IFormFile> formfiles)
        {
            List<Blob> blobs = new List<Blob>();
            foreach (IFormFile ff in formfiles)
                if (ff != null)
                {
                    blobs.Add(FormFileToBlob(ff));
                }
            return blobs;
        }

        public static IFormFile BlobToFormFile(Blob blob)
        {
            using (var ms = new MemoryStream(blob.Buffer))
            {
                var returnFormFile = new FormFile(ms, 0, blob.lenth, blob.name, blob.filename);

                return returnFormFile;
            }
        }

        public static IEnumerable<IFormFile> BlobsToFormFiles(IEnumerable<Blob> blobs)
        {
            List<IFormFile> formFiles = new List<IFormFile>();
            foreach(var blob in blobs)
            {
                formFiles.Add(BlobToFormFile(blob));
            }
            return formFiles;
        }
    }
}
