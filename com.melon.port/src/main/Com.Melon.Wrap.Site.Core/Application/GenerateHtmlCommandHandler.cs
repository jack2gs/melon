using Com.Melon.Wrap.Site.Core.Port.Adapter.Markdown;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Melon.Wrap.Site.Core.Application
{
    public class GenerateHtmlCommandHandler : IRequestHandler<GenerateHtmlCommand, string>
    {
        private readonly IMarkdownService _markdownService;

        public GenerateHtmlCommandHandler(IMarkdownService markdownService)
        {
            _markdownService = markdownService;
        }

        public Task<string> Handle(GenerateHtmlCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_markdownService.ConvertToHtml(request.Markdown));
        }
    }
}
