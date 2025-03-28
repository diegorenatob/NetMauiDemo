namespace MyBlog.Domain.Entities
{
    public class Post
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }

        private Post() { }

        public Post(int id, string title, string body)
        {
            Id = id;
            Title = title;
            Body = body;
        }

        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("Title cannot be empty.");

            Title = newTitle;
        }

        public void UpdateBody(string newBody)
        {
            if (string.IsNullOrWhiteSpace(newBody))
                throw new ArgumentException("Body cannot be empty.");

            Body = newBody;
        }
    }
}