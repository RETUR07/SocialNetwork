﻿using Application.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IRateService
    {
        public Task<RateForResponseDTO> GetRateAsync(int userId, int postId, bool trackChanges);
        public Task<IEnumerable<RateForResponseDTO>> GetRatesByUserIdAsync(int userId, bool trackChanges);
        public Task<IEnumerable<RateForResponseDTO>> GetRatesByPostIdAsync(int postId, bool trackChanges);
        public Task<RateForResponseDTO> CreateRateAsync(RateForm rate);
        public Task<bool> UpdateUserAsync(RateForm rate);
    }
}
