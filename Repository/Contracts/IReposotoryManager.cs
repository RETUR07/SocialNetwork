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
        IBlobRepository blob { get; }
        IRateRepository rate { get; }
        Task SaveAsync();
    } 
}

