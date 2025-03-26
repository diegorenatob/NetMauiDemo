using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyBlog.Infrastructure.Data
{
    public class MyBlogDbContextFactory : IDesignTimeDbContextFactory<MyBlogDbContext>
    {
        public MyBlogDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MyBlogDbContext>();

            builder.UseSqlite("Data Source=LocalDesignTime.db");

            return new MyBlogDbContext(builder.Options);
        }
    }
}
