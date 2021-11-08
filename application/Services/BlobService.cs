using Application.Contracts;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using ProjectRepository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BlobService : IBlobService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public BlobService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IFormFile>> GetBLobsAsync(IEnumerable<int> Ids, bool trackChanges)
        {
            List<IFormFile> formfiles = new List<IFormFile>(); 
            foreach (int id in Ids)
            {
                var blob = await _repository.blob.GetBlob(id, trackChanges);
                if (blob == null)
                    continue;
                var FormFile = Converters.BlobToFormFile(blob);
                formfiles.Add(FormFile);
            }
            return formfiles;

        }

        public async Task<IEnumerable<int>> SaveBlobsAsync(IEnumerable<IFormFile> formFiles)
        {
            List<Blob> blobs = new List<Blob>();
            foreach (IFormFile formfile in formFiles)
            {
                if (formfile == null)
                    continue;
                var blob = await Converters.FormFileToBlobAsync(formfile);
                _repository.blob.CreateBlob(blob);
                blobs.Add(blob);
            }
            await _repository.SaveAsync();
            return blobs.Select(b => b.Id);
        }
    }
}
