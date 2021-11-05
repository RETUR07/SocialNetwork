using ProjectRepository.Contracts;
using System.Threading.Tasks;

namespace ProjectRepository.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;

        private IUserRepository _userRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IUserRepository user
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_repositoryContext);
                return _userRepository;
            }
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
