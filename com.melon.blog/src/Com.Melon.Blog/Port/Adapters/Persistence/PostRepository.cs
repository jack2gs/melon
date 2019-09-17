using Com.Melon.Blog.Domain;
using System.Linq;

namespace Com.Melon.Blog.Port.Adapters.Persistence
{
    public class PostRepository : IPostRepository
    {
        private BlogDbContext _blogDbContext;

        public PostRepository(BlogDbContext blogDbContext)
        {
            this._blogDbContext = blogDbContext;
        }

        public Post GetById(int id)
        {
            return _blogDbContext.Posts.SingleOrDefault(x => x.Id == id);
        }

        public void Save(Post post)
        {
            _blogDbContext.Posts.Add(post);
            _blogDbContext.SaveChanges();
        }
    }
}
