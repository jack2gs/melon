using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Melon.Blog.Models
{
    public class Post
    {
        public int PostId { get; private set; }

        public int AuthorId { get; private set; }

        public string Content { get; private set; }

        internal Post(int authorId, string content)
        {
            AuthorId = authorId;
            Content = content;
        }
    }
}
