using MediatR;

namespace Com.Melon.Blog.Application.Commands.ExceptPost
{
    public class ExceptPostCommand: IRequest<string>
    {
        public string PostContent { get; }

        public ExceptPostCommand(string postContent)
        {
            PostContent = postContent;
        }
    }
}