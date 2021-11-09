using Application.Contracts;
using Application.DTO;
using AutoMapper;
using Entities.Models;
using ProjectRepository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RateService : IRateService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public RateService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RateForResponseDTO> CreateRateAsync(RateForm ratedto)
        {
            if (ratedto == null)
            {
                return null;
            }
            var rate = _mapper.Map<RateForm, Rate>(ratedto);
            _repository.rate.Create(rate);
            await _repository.SaveAsync();
            return _mapper.Map <Rate, RateForResponseDTO>(rate);
        }

        public async Task<RateForResponseDTO> GetRateAsync(int userId, int postId, bool trackChanges)
        {
            var rate = await _repository.rate.GetRateAsync(userId, postId, trackChanges);
            return _mapper.Map<Rate, RateForResponseDTO>(rate);
        }

        public async Task<bool> UpdateUserAsync(int userId, int postId, RateForm ratedto)
        {
            if (ratedto == null) return false;
            var rate = await _repository.rate.GetRateAsync(userId, postId, true);
            if (rate == null) return false;
            _mapper.Map(ratedto, rate);
            await _repository.SaveAsync();
            return true;
        }
    }
}
