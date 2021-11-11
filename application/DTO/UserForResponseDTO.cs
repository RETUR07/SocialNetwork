﻿using System.Collections.Generic;

namespace SocialNetwork.Application.DTO
{
    public class UserForResponseDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<int> Friends { get; set; }
        public List<int> Subscribers { get; set; }
        public string DateOfBirth { get; set; }
    }
}
