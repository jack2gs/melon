using Melon.Blog.Areas.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Melon.Blog.Areas.Blog.Data
{
    public class Post
    {
        public int PostId { get; private set; } 

        public int AuthorId { get; private set; }

        public string Content { get; private set; }

        public Post(string content)
        {
            this.Content = content;
        }

        public void AssignAuthor(Author author)
        {
            if (author.AuthorStatus != AuthorStatus.Active)
            {
                throw new ArgumentException("the author is not active yet.");
            }

            AuthorId = author.AuthorId;
        }
    }
}
