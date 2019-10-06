using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Com.Melon.Core.Framework.Pagination;

namespace Com.Melon.Blog.Domain
{
    public interface IPostRepository
    {
        void Save(Post post);

        Post GetById(int id);

        Task<PagedResult<Post>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken));

        Task<int> CountAsync();
        
        Task UpdateAsync(Post post, CancellationToken cancellationToken = default(CancellationToken));
    }
}
