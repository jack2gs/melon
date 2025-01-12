﻿using System;

namespace Com.Melon.Blog.Application
{
    public class PostData
    {
        public int PostId { get; }

        public string Title { get; }

        public string Content { get; }
        
        public DateTime DateTimeCreated { get; }
        
        public DateTime DateTimeLastModified { get; }

        public PostData(int postId,
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
