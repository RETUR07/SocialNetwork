using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public class BlobService : IBlobService
    {
        private readonly IRepositoryManager _repository;
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(IRepositoryManager repository, BlobServiceClient blobServiceClient)
        {
            _repository = repository;
            _blobServiceClient = blobServiceClient;
        }

        public async Task<List<FileContentResult>> GetBLobsAsync(IEnumerable<int> Ids, bool trackChanges)
        {
            List<FileContentResult> formfiles = new List<FileContentResult>(); 
            foreach (int id in Ids)
            {
                var blob = await _repository.Blob.GetBlob(id, trackChanges);
                if (blob == null)
                    continue;


                var blobContainer = _blobServiceClient.GetBlobContainerClient("files");

                var blobClient = blobContainer.GetBlobClient(blob.Filename);
                var downloadContent = await blobClient.DownloadAsync();
                using (MemoryStream ms = new MemoryStream())
                {
                    await downloadContent.Value.Content.CopyToAsync(ms);
                    var FormFile = BlobConverters.BlobToFileContentResult(blob, ms.ToArray());
                    formfiles.Add(FormFile);                  
                }         
            }
            return formfiles;

        }

        public async Task<List<List<FileContentResult>>> GetBLobsAsync(IEnumerable<IEnumerable<int>> collection, bool trackChanges)
        {
            List<List<FileContentResult>> collectionfiles = new List<List<FileContentResult>>();
            var blobContainer = _blobServiceClient.GetBlobContainerClient("files");

            foreach (IEnumerable<int> Ids in collection)
            {
                var blobs = _repository.Blob.FindByCondition(x => Ids.Contains(x.Id), trackChanges);
                List<FileContentResult> formfiles = new List<FileContentResult>();

                foreach (Blob blob in blobs)
                {              
                    if (blob == null)
                        continue;

                    var blobClient = blobContainer.GetBlobClient(blob.Filename);
                    var downloadContent = await blobClient.DownloadAsync();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await downloadContent.Value.Content.CopyToAsync(ms);
                        var FormFile = BlobConverters.BlobToFileContentResult(blob, ms.ToArray());
                        formfiles.Add(FormFile);
                    }
                }
                collectionfiles.Add(formfiles);
            }
            return collectionfiles;

        }

        public async Task<List<int>> SaveBlobsAsync(IEnumerable<IFormFile> formFiles)
        {
            List<Blob> blobs = new List<Blob>();
            foreach (IFormFile formfile in formFiles)
            {
                if (formfile == null)
                    continue;
                var blob = await BlobConverters.FormFileToBlobAsync(formfile);
                _repository.Blob.Create(blob);

                var blobContainer = _blobServiceClient.GetBlobContainerClient("files");
                var blobClient = blobContainer.GetBlobClient(formfile.FileName);
                await blobClient.UploadAsync(formfile.OpenReadStream());

                blobs.Add(blob);
            }
            await _repository.SaveAsync();
            return blobs.Select(b => b.Id).ToList();
        }
    }
}
