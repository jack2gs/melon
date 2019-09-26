namespace Com.Melon.Blog.Application
{
    public class PostData
    {
        public int PostId { get; }

        public string Title { get; }

        public string Content { get; }

        public PostData(int postId, string title, string content)
        {
            PostId = postId;
            Title = title;
            Content = content;
        }
    }
}
