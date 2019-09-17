namespace Com.Melon.Blog.Domain
{
    public interface IPostRepository
    {
        void Save(Post post);

        Post GetById(int id);
    }
}
