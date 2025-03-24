using MyBlog.Domain.Entities;

namespace MyBlog.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(int id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(int id);

        // New method: add a batch of posts if they don't exist already
        Task AddRangeIfNotExistsAsync(IEnumerable<Post> posts);
    }
}