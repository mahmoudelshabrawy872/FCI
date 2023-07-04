using FCI_API.Models;
using FCI_DataAccess.Repository.IRepository;

namespace FCI_Business
{
    public class PostData
    {
        private readonly IPostRepository _postRepository;
        PostData(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> Posts()
        {
            return await _postRepository.GetAllAsync();
        }

    }
}
