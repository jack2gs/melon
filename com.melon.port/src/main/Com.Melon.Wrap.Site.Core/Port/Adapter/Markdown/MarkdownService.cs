using Markdig;

namespace Com.Melon.Wrap.Site.Core.Port.Adapter.Markdown
{
    public class MarkdownService : IMarkdownService
    {
        private readonly MarkdownPipeline markdownPipeline;

        public MarkdownService(){
            // Configure the pipeline with all advanced extensions active
            markdownPipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseBootstrap()
                .Build();
        }

        public string ConvertToHtml(string markdown)
        {
            return Markdig.Markdown.ToHtml(markdown, markdownPipeline);
        }
    }
}
