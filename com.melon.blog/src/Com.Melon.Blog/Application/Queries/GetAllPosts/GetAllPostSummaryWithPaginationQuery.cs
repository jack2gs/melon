using System.Collections.Generic;
using Com.Melon.Core.Framework.Pagination;
using MediatR;

namespace Com.Melon.Blog.Application.Queries.GetAllPosts
{
    public class GetAllPostSummaryWithPaginationQuery: IRequest<PagedResult<PostData>>
    {
       public int PageIndex { get; } 
       
       public int PageSize { get; }

       public GetAllPostSummaryWithPaginationQuery(int pageIndex, int pageSize)
       {
           PageIndex = pageIndex;
           PageSize = pageSize;
       }
    }
}