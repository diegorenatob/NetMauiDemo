using MyBlog.Application.DTOs;
using MyBlog.Application.Interfaces;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Interfaces;

namespace MyBlog.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IExternalPostService _externalService; 

        public PostService(IPostRepository postRepository, IExternalPostService externalService)
        {
            _postRepository = postRepository;
            _externalService = externalService; 
        }


        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Body = p.Body
            });
        }

        public Task<PostDto?> GetPostByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreatePostAsync(PostDto postDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostAsync(PostDto postDto)
        {
            throw new NotImplementedException();
        }

        public Task DeletePostAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task FetchAndStoreNewPostsAsync()
        {
            // 2) Use the interface method
            var remotePosts = await _externalService.FetchPostsAsync();

            var domainPosts = remotePosts.Select(rp => new Post(rp.Id, rp.Title, rp.Body)).ToList();
            await _postRepository.AddRangeIfNotExistsAsync(domainPosts);
        }
    }
}