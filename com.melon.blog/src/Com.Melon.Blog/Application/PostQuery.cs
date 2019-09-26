using Com.Melon.Blog.Domain;
using MediatR;

namespace Com.Melon.Blog.Application
{
    public class PostQuery: IRequest<PostData>
    {
        public int PostId { get; }

        public PostQuery(int postId)
        {
            PostId = postId;
        }
    }
}
