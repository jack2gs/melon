using System.Threading;
using System.Threading.Tasks;
using Com.Melon.Blog.Domain;
using MediatR;

namespace Com.Melon.Blog.Application
{
    public class PostQueryHandler : IRequestHandler<PostQuery, PostData>
    {
        private IPostRepository postRepository;

        public PostQueryHandler(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public Task<PostData> Handle(PostQuery request, CancellationToken cancellationToken)
        {
            var post = postRepository.GetById(request.PostId);

            if (post == null)
            {
                return Task.FromResult<PostData>(null);
            }

            return Task.FromResult(new PostData(post.Id, post.Title, post.Content, post.DateTimeCreated, post.DateTimeLastModified));
        }
    }
}
