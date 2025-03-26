using Moq;
using MyBlog.Domain.Interfaces;
using MyBlog.Domain.Entities;
using MyBlog.Application.Interfaces;
using MyBlog.Application.Services;
using MyBlog.Application.DTOs;

namespace MyBlog.Tests.Application
{
    [TestFixture]
    public class PostServiceTests
    {
        private Mock<IPostRepository> _mockPostRepository;
        private Mock<IExternalPostService> _mockExternalService;
        private IPostService _postService;
        private Mock<IConnectivityService> _mockConnectivityService;
        [SetUp]
        public void SetUp()
        {
            _mockPostRepository = new Mock<IPostRepository>();
            _mockExternalService = new Mock<IExternalPostService>();
            _mockConnectivityService = new Mock<IConnectivityService>();
            _postService = new PostService(_mockPostRepository.Object, _mockExternalService.Object,_mockConnectivityService.Object);
        }

        [Test]
        public async Task GetAllPostsAsync_WhenCalled_ReturnsAllPosts()
        {
            // Arrange
            var domainPosts = new List<Post>
            {
                new Post(1, "Title1", "Body1"),
                new Post(2, "Title2", "Body2")
            };
            _mockPostRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(domainPosts);

            // Act
            var result = await _postService.GetAllPostsAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Title, Is.EqualTo("Title1"));
        }

        [Test]
        public async Task CreatePostAsync_WhenCalled_InvokesAddOnRepository()
        {
            var newPostDto = new PostDto { Id = 3, Title = "Title3", Body = "Body3" };

            await _postService.CreatePostAsync(newPostDto);

         
            _mockPostRepository.Verify(r => r.AddAsync(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public async Task FetchAndStoreNewPostsAsync_WhenCalled_FetchesAndAddsNewPosts()
        {
            var externalPosts = new List<PostDto>
            {
                new PostDto { Id = 100, Title = "External Title", Body = "Some Body" }
            };
            _mockExternalService.Setup(s => s.FetchPostsAsync()).ReturnsAsync(externalPosts);

            await _postService.FetchAndStoreNewPostsAsync();

            _mockPostRepository.Verify(r => r.AddRangeIfNotExistsAsync(It.Is<IEnumerable<Post>>(posts => posts.Any(p => p.Id == 100))), Times.Once);
        }
    }
}
