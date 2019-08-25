using Melon.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Melon.Blog.Areas.Blog.Models
{
    public class Author
    {
        public int AuthorId { get; private set; }

        public string FirstName { get; private set; }

        public string Surname { get; private set; }

        public AuthorStatus AuthorStatus {get; private set;}

        public Author(int authorId, string firstName, string surName)
        {
            AuthorId = authorId;
            FirstName = firstName;
            Surname = surName; 
        }

        public Post NewPost()
        {
            if (AuthorStatus != AuthorStatus.Active)
            {
                throw new InvalidOperationException("The author could not create new post, as it's not active yet.");
            }

            return null; 
        }
    }
}
