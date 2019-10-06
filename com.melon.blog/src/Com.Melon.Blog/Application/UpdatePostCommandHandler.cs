using System;
using System.Threading;
using System.Threading.Tasks;
using Com.Melon.Blog.Domain;
using MediatR;

namespace Com.Melon.Blog.Application
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
    {
        private IPostRepository postRepository;

        public UpdatePostCommandHandler(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            Post post = postRepository.GetById(request.PostId);

            // it doesn't leak the domain knowledge here
            // the applciation service just coordiate the domain objects
            // because if we remove the following line, no big deal to the BAU process
            if (post == null)
            {
                throw new ArgumentException("The post doesn't exist.");
            }

            post.Update(request.Title, request.Content);

            await postRepository.UpdateAsync(post, cancellationToken);

            return Unit.Value;
        }
    }
}
