using AutoMapper;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
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

            var user = await _repository.User.GetUserAsync(ratedto.UserId, true);
            var post = await _repository.Post.GetPostAsync(ratedto.PostId, true);

            var rate = _mapper.Map<RateForm, Rate>(ratedto);
            _mapper.Map(user, rate);
            _mapper.Map(post, rate);

            _repository.Rate.Create(rate);
            await _repository.SaveAsync();
            return _mapper.Map<Rate, RateForResponseDTO>(rate);
        }

        public async Task<RateForResponseDTO> GetRateAsync(int userId, int postId, bool trackChanges)
        {
            var rate = await _repository.Rate.GetRateAsync(userId, postId, trackChanges);
            return _mapper.Map<Rate, RateForResponseDTO>(rate);
        }

        public async Task<IEnumerable<RateForResponseDTO>> GetRatesByPostIdAsync(int postId, bool trackChanges)
        {
            var rates = await _repository.Rate.GetRatesByPostIdAsync(postId, trackChanges);
            return _mapper.Map<IEnumerable<Rate>, IEnumerable<RateForResponseDTO>>(rates);
        }

        public async Task<IEnumerable<RateForResponseDTO>> GetRatesByUserIdAsync(int userId, bool trackChanges)
        {
            var rates = await _repository.Rate.GetRatesByPostIdAsync(userId, trackChanges);
            return _mapper.Map<IEnumerable<Rate>, IEnumerable<RateForResponseDTO>>(rates);
        }

        public async Task UpdateRateAsync(RateForm ratedto)
        {
            if (ratedto == null) return;
            var rate = await _repository.Rate.GetRateAsync(ratedto.UserId, ratedto.PostId, true);
            if (rate == null)
            {
                var user = await _repository.User.GetUserAsync(ratedto.UserId, true);
                var post = await _repository.Post.GetPostAsync(ratedto.PostId, true);

                rate = _mapper.Map<RateForm, Rate>(ratedto);
                _mapper.Map(user, rate);
                _mapper.Map(post, rate);
                _repository.Rate.Create(rate);
            }
            else
            {
                if (ratedto.LikeStatus == rate.LikeStatus)
                    _repository.Rate.Delete(rate);
                else
                    _mapper.Map(ratedto, rate);               
            }
            await _repository.SaveAsync();
            return;
        }
    }
}
