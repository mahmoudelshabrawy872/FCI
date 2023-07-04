using FCI_API.Models;

namespace FCI_DataAccess.Repository.IRepository
{
    /// <summary>
    /// Interface for the post repository, extending the generic repository.
    /// </summary>
    public interface IPostRepository : IRepository<Post>
    {
        /// <summary>
        /// Updates a post asynchronously.
        /// </summary>
        /// <param name="post">The updated post object.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the updated post entity.</returns>
        public Task<Post> UpdatePostAsync(Post post);
    }
}