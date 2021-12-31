using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System;
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

        public async Task<List<Uri>> GetBLobsAsync(IEnumerable<int> Ids, bool trackChanges)
        {
            List<Uri> formfiles = new List<Uri>(); 
            foreach (int id in Ids)
            {
                var blob = await _repository.Blob.GetBlob(id, trackChanges);
                if (blob == null)
                    continue;


                var blobContainer = _blobServiceClient.GetBlobContainerClient("files");

                var blobClient = blobContainer.GetBlobClient(blob.Filename);
                var downloadContent = blobClient.GenerateSasUri(BlobSasPermissions.Read, new DateTimeOffset(DateTime.Now.AddMinutes(10)));
                formfiles.Add(downloadContent);                  
            }
            return formfiles;

        }

        public List<List<Uri>> GetBLobsAsync(IEnumerable<IEnumerable<int>> collection, bool trackChanges)
        {
            List<List<Uri>> collectionfiles = new List<List<Uri>>();
            var blobContainer = _blobServiceClient.GetBlobContainerClient("files");

            foreach (IEnumerable<int> Ids in collection)
            {
                var blobs = _repository.Blob.FindByCondition(x => Ids.Contains(x.Id), trackChanges);
                List<Uri> formfiles = new List<Uri>();

                foreach (Blob blob in blobs)
                {              
                    if (blob == null)
                        continue;

                    var blobClient = blobContainer.GetBlobClient(blob.Filename);
                    var downloadContent = blobClient.GenerateSasUri(BlobSasPermissions.Read, new DateTimeOffset(DateTime.Now.AddMinutes(10)));
                    formfiles.Add(downloadContent);
                }
                collectionfiles.Add(formfiles);
            }
            return collectionfiles;
        }

        public async Task<List<Blob>> SaveBlobsAsync(IEnumerable<IFormFile> formFiles, string uniqueID)
        {
            if (formFiles == null)return new List<Blob>();
            List<Blob> blobs = new List<Blob>();
            foreach (IFormFile formfile in formFiles)
            {
                if (formfile == null)
                    continue;
                var blob = await BlobConverters.FormFileToBlobAsync(formfile);
                blob.Filename = uniqueID + "-" + formfile.FileName;
                _repository.Blob.Create(blob);

                var blobContainer = _blobServiceClient.GetBlobContainerClient("files");
                var blobClient = blobContainer.GetBlobClient(uniqueID + "-" + formfile.FileName);
                await blobClient.UploadAsync(formfile.OpenReadStream());

                blobs.Add(blob);
            }
            return blobs;
        }
    }
}
