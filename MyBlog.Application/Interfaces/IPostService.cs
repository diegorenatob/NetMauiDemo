using MyBlog.Application.DTOs;

namespace MyBlog.Application.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllPostsAsync();
        Task<PostDto?> GetPostByIdAsync(int id);
        Task CreatePostAsync(PostDto postDto);
        Task UpdatePostAsync(PostDto postDto);
        Task DeletePostAsync(int id);
        Task FetchAndStoreNewPostsAsync();
    }
}