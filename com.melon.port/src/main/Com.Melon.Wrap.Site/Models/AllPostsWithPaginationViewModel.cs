namespace Com.Melon.Wrap.Site.Models
{
    public class AllPostsWithPaginationViewModel
    {
        public PagedCollection<PostItemViewModel> Posts { get; }

        public AllPostsWithPaginationViewModel(PagedCollection<PostItemViewModel> posts)
        {
            Posts = posts;
        }
    }
}