using System;

namespace Com.Melon.Wrap.Site.Models
{
    public class PostItemViewModel
    {
        public int PostId { get; }

        public string Title { get; }

        public string Content { get; }
        
        public DateTime DateTimeCreated { get; }
        
        public DateTime DateTimeLastModified { get; }

        public PostItemViewModel(int postId,
            string title,
            string content,
            DateTime dateTimeCreated,
            DateTime dateTimeLastModified)
        {
            PostId = postId;
            Title = title;
            Content = content;
            DateTimeCreated = dateTimeCreated;
            DateTimeLastModified = dateTimeLastModified;
        }
    }
}
