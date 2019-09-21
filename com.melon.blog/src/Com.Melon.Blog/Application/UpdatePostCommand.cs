using MediatR;

namespace Com.Melon.Blog.Application
{
    public class UpdatePostCommand: IRequest
    {
       public int PostId { get; } 

       public string Title { get; }

       public string Content { get; }

       public UpdatePostCommand(int postId, string title, string content)
       {
            PostId = postId;
            Title = title;
            Content = content;
        }
    }
}
