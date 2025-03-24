using MyBlog.Application.DTOs;

namespace MyBlog.Application.Interfaces
{
    public interface IExternalPostService
    {
        Task<List<PostDto>> FetchPostsAsync();
    }
}