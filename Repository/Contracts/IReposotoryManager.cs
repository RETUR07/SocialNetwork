using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRepository.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository user { get; }
        IPostRepository post { get; }
        Task SaveAsync();
    } 
}

