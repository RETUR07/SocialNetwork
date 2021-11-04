using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository user { get; }
        Task SaveAsync();
    } 
}

