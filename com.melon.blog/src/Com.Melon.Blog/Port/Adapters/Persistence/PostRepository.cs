using System.Collections.Generic;
using Com.Melon.Blog.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Com.Melon.Core.Framework.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Com.Melon.Blog.Port.Adapters.Persistence
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public PostRepository(BlogDbContext blogDbContext)
        {
            this._blogDbContext = blogDbContext;
        }

        public Post GetById(int id)
        {
            return _blogDbContext.Posts.SingleOrDefault(x => x.Id == id);
        }

        public async Task<PagedResult<Post>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryable = _blogDbContext.Posts.AsQueryable();
            var totalCount = await queryable.CountAsync(cancellationToken);
            var results = await queryable.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            
            return new PagedResult<Post>(results, totalCount);
        }

        public Task<int> CountAsync()
        {
            return _blogDbContext.Posts.CountAsync();
        }

        public Task UpdateAsync(Post post, CancellationToken cancellationToken = default(CancellationToken))
        {
            _blogDbContext.Posts.Update(post);
            return _blogDbContext.SaveChangesAsync(cancellationToken);
        }

        public void Save(Post post)
        {
            _blogDbContext.Posts.Add(post);
            _blogDbContext.SaveChanges();
        }
    }
}
