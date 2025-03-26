using MyBlog.Domain.Entities;

namespace MyBlog.Tests.Domain
{
    [TestFixture]
    public class PostTests
    {
        [Test]
        public void UpdateTitle_WithValidTitle_UpdatesSuccessfully()
        {
            // Arrange
            var post = new Post(1, "Old Title", "Test body");

            // Act
            post.UpdateTitle("New Title");

            // Assert
            Assert.AreEqual("New Title", post.Title);
        }

        [Test]
        public void UpdateTitle_WithEmptyTitle_ThrowsArgumentException()
        {
            // Arrange
            var post = new Post(1, "Old Title", "Test body");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => post.UpdateTitle(""));
        }
    }
}