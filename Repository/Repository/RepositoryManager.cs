using ProjectRepository.Contracts;
using System.Threading.Tasks;

namespace ProjectRepository.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;

        private IUserRepository _userRepository;
        private IPostRepository _postRepository;
        private IBlobRepository _blobRepository;

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

        public IPostRepository post
        {
            get
            {
                if (_postRepository == null)
                    _postRepository = new PostRepository(_repositoryContext);
                return _postRepository;
            }
        }

        public IBlobRepository blob
        {
            get
            {
                if (_blobRepository == null)
                    _blobRepository = new BlobRepository(_repositoryContext);
                return _blobRepository;
            }
        }
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
