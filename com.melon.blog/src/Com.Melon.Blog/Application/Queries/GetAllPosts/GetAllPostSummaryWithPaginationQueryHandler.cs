using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Com.Melon.Blog.Domain;
using Com.Melon.Core.Framework.Pagination;
using MediatR;

namespace Com.Melon.Blog.Application.Queries.GetAllPosts
{
    public class GetAllPostSummaryWithPaginationQueryHandler: IRequestHandler<GetAllPostSummaryWithPaginationQuery, PagedResult<PostData>>
    {
        private readonly IPostRepository _postRepository;

        public GetAllPostSummaryWithPaginationQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PagedResult<PostData>> Handle(GetAllPostSummaryWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetAllAsync(request.PageIndex, request.PageSize, cancellationToken);

            return new PagedResult<PostData>(posts.Items.Select(x => new PostData(x.Id, x.Title, x.ExcerptContent(), x.DateTimeCreated, x.DateTimeLastModified)), posts.TotalItemsCount);
        }
    }
}