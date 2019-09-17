using Com.Melon.Core.Domain;

namespace Com.Melon.Blog.Domain
{
    public class Post: AggregateRoot<Post>
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            private set
            {
                string title = value; // sometimes, copy it first to avoid the concurrent issue.
                SelfAssertArgumentNotNull(title, "Please set the title of the post.");
                SelfAssertArgumentLength(title, 1, 100, "The length of title should between 1 and 100.");
                SelfAssertArgumentMatches("^[\u4e00-\u9fa5_a-zA-Z0-9 ]+$", title, "The title should just contains characters, numbers or underscore.");
                _title = title;
            }
        }

        public string Content { get; private set; }

        private Post()
        {

        }

        public Post(string title, string content)
        {

            Title = title;
            Content = content;
        }
    }
}
