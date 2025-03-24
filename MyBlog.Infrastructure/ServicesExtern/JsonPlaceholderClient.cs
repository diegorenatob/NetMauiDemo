using MyBlog.Application.DTOs;
using MyBlog.Application.Interfaces;
using System.Net.Http.Json;

namespace MyBlog.Infrastructure.ServicesExtern
{
    public class JsonPlaceholderClient : IExternalPostService
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceholderClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            if (_httpClient.BaseAddress == null)
                _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        }

        public async Task<List<PostDto>> FetchPostsAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<PostDto>>("posts");
            return result ?? new List<PostDto>();
        }
    }
}