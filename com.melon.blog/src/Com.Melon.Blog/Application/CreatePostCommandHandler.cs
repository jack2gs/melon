using Com.Melon.Blog.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Melon.Blog.Application
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
    {
        private readonly IPostRepository _postRepository;

        public CreatePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            _postRepository.Save(new Post(request.Title, request.Content));

            return Unit.Task;
        }
    }
}
