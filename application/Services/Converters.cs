using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Entities.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public static class Converters
    {
        public static async Task<Blob> FormFileToBlobAsync(this IFormFile formfile)
        {
            using (var ms = new MemoryStream())
            {
                if (formfile == null) return null;

                Blob blob = new Blob();

                await formfile.CopyToAsync(ms);
                blob.Buffer = ms.ToArray();
                blob.Filename = formfile.FileName;
                blob.Name = formfile.Name;
                blob.Lenth = formfile.Length;
                blob.ContentType = formfile.ContentType;
                return blob;
            }
        }

        public static Blob FormFileToBlob(this IFormFile formfile)
        {
            using (var ms = new MemoryStream())
            {
                if (formfile == null) return null;

                Blob blob = new Blob();

                formfile.CopyTo(ms);
                blob.Buffer = ms.ToArray();
                blob.Filename = formfile.FileName;
                blob.Name = formfile.Name;
                blob.Lenth = formfile.Length;
                blob.ContentType = formfile.ContentType;
                return blob;
            }
        }

        public static async Task<IEnumerable<Blob>> FormFilesToBlobsAsync(this IEnumerable<IFormFile> formfiles)
        {
            List<Blob> blobs = new List<Blob>();
            foreach (IFormFile ff in formfiles)
                if (ff != null)
                {
                    blobs.Add(await ff.FormFileToBlobAsync());
                }
            return blobs;
        }

        public static IEnumerable<Blob> FormFilesToBlobs(this IEnumerable<IFormFile> formfiles)
        {
            List<Blob> blobs = new List<Blob>();
            foreach (IFormFile ff in formfiles)
                if (ff != null)
                {
                    blobs.Add(ff.FormFileToBlob());
                }
            return blobs;
        }

        public static FileContentResult BlobToFileContentResult(this Blob blob)
        {
            var returnfile = new FileContentResult(blob.Buffer, blob.ContentType);
            returnfile.FileDownloadName = blob.Filename;
            return returnfile;
        }

        public static IEnumerable<FileContentResult> BlobsToFileContentResults(this IEnumerable<Blob> blobs)
        {
            List<FileContentResult> files = new List<FileContentResult>();
            foreach(var blob in blobs)
            {
                files.Add(blob.BlobToFileContentResult());
            }
            return files;
        }
    }
}
