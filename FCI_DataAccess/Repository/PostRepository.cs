using FCI_API.Data;
using FCI_API.Models;
using FCI_DataAccess.Repository.IRepository;

namespace FCI_DataAccess.Repository
{
    /// <summary>
    /// Repository implementation for the Post entity.
    /// </summary>
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Updates a post asynchronously.
        /// </summary>
        /// <param name="post">The updated post object.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the updated post entity.</returns>
        public async Task<Post> UpdatePostAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }
    }
}