using Microsoft.EntityFrameworkCore;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Interfaces;
using MyBlog.Infrastructure.Data;

namespace MyBlog.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MyBlogDbContext _dbContext;

        public PostRepository(MyBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _dbContext.Posts.FindAsync(id);
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _dbContext.Posts.ToListAsync();
        }

        public async Task AddAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddRangeIfNotExistsAsync(IEnumerable<Post> posts)
        {
            foreach (var post in posts)
            {
                bool exists = await _dbContext.Posts.AnyAsync(p => p.Id == post.Id);
                if (!exists)
                {
                    await _dbContext.Posts.AddAsync(post);
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}