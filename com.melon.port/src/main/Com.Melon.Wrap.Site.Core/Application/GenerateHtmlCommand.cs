using MediatR;

namespace Com.Melon.Wrap.Site.Core.Application
{
    public class GenerateHtmlCommand: IRequest<string>
    {
        public string Markdown { get; }

        public GenerateHtmlCommand(string markdown)
        {
            Markdown = markdown;
        }
    }
}
