using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public class BlobService : IBlobService
    {
        private readonly IRepositoryManager _repository;
        public BlobService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<List<FileContentResult>> GetBLobsAsync(IEnumerable<int> Ids, bool trackChanges)
        {
            List<FileContentResult> formfiles = new List<FileContentResult>(); 
            foreach (int id in Ids)
            {
                var blob = await _repository.Blob.GetBlob(id, trackChanges);
                if (blob == null)
                    continue;
                var FormFile = BlobConverters.BlobToFileContentResult(blob);
                formfiles.Add(FormFile);
            }
            return formfiles;

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
                blobs.Add(blob);
            }
            await _repository.SaveAsync();
            return blobs.Select(b => b.Id).ToList();
        }
    }
}
