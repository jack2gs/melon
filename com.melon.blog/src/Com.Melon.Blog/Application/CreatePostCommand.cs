using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Com.Melon.Blog.Application
{
    public class CreatePostCommand: IRequest
    {
        public string Title { get; private set; }

        public string Content { get; private set; }

        public IReadOnlyCollection<int> Tags { get; private set; }

        public IReadOnlyCollection<int> Categories { get; private set; }

        public CreatePostCommand(string title, string content, IEnumerable<int> tags, IEnumerable<int> categories)
        {
            Title = title;
            Content = content;
            Tags = tags.ToList<int>().AsReadOnly();
            Categories = categories.ToList<int>().AsReadOnly();
        }

        public CreatePostCommand(string title, string content) : this(title, content, new List<int>(), new List<int>())
        {
        }
    }
}
