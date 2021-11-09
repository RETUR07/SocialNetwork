using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        IPostRepository Post { get; }
        IBlobRepository Blob { get; }
        IRateRepository Rate { get; }
        ICommentRepository Comment { get; }
        Task SaveAsync();
    } 
}

