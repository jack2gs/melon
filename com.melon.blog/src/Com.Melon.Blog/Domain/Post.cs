using System;
using Com.Melon.Core.Domain;
using Com.Melon.Core.Infrastructure;

namespace Com.Melon.Blog.Domain
{
    public class Post: AggregateRoot<Post>
    {
        private string _title;

        private const string ExcerptSeparator = "<!--more-->";

        public string Title
        {
            get { return _title; }
            private set
            {
                string title = value; // sometimes, copy it first to avoid the concurrent issue.
                SelfAssertArgumentNotNull(title, "Please set the title of the post.");
                SelfAssertArgumentNotEmpty(title, "Please set the title of the post.");
                SelfAssertArgumentLength(title, 1, 100, "The length of title should between 1 and 100.");
                SelfAssertArgumentMatches("^[\u4e00-\u9fa5_a-zA-Z0-9 ]+$", title, "The title should just contains characters, numbers or underscore.");
                _title = title;
            }
        }

        public void Update(string title, string content)
        {
            Title = title;
            Content = content;
            DateTimeLastModified = Clock.Now;
        }

        public string Content { get; private set; }

        private Post()
        {

        }

        public Post(string title, string content): this(0, title, content)
        {

        }

        public Post(int id, string title, string content)
            :this(id, title, content,Clock.Now,Clock.Now)
        {
        }
        
        public Post(int id, string title, string content, DateTime dateTimeCreated, DateTime dateTimeLastModified)
            :base(id, dateTimeCreated, dateTimeLastModified)
        {
            Title = title;
            Content = content;
        }

        public string ExcerptContent()
        {
            int index = Content.IndexOf(ExcerptSeparator, StringComparison.Ordinal);

            if (index < 0)
            {
                index = Math.Min(300, Content.Length);
            }

            return Content.Substring(0, index);
        }
    }
}
