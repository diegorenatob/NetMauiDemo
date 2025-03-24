using Microsoft.EntityFrameworkCore;
using MyBlog.Domain.Entities;

namespace MyBlog.Infrastructure.Data
{
    public class MyBlogDbContext : DbContext
    {
        public DbSet<Post> Posts => Set<Post>();

        public MyBlogDbContext(DbContextOptions<MyBlogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}