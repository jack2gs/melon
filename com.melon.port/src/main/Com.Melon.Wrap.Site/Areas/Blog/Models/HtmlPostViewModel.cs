namespace Com.Melon.Wrap.Site.Areas.Blog.Models
{
    public class HtmlPostViewModel
    {
        public int PostId { get; }

        public string Title { get; }

        public string HtmlContent { get; }

        public HtmlPostViewModel(int postId, string title, string htmlContent)
        {
            PostId = postId;
            Title = title;
            HtmlContent = htmlContent;
        }
    }
}
