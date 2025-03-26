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
        Task AddRangeIfNotExistsAsync(IEnumerable<Post> posts);
    }
}