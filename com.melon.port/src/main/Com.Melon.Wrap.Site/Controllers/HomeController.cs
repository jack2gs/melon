using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Com.Melon.Wrap.Site.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Com.Melon.Blog.Application;
using Com.Melon.Blog.Application.Queries.GetAllPosts;
using Com.Melon.Wrap.Site.Core.Application;

namespace Com.Melon.Wrap.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            if (pageSize < 10 || pageSize > 100)
            {
                pageSize = 20;
            }
            
            var posts = await _mediator.Send(new GetAllPostSummaryWithPaginationQuery(pageIndex, pageSize), default(CancellationToken));
            var postViewModelsTasks = posts.Items
                .Select(async x => new PostItemViewModel(x.PostId, x.Title,  await GenerateHtmlContent(x), x.DateTimeCreated, x.DateTimeLastModified));
            var postViewModels = await Task.WhenAll(postViewModelsTasks);
            var viewModel = new AllPostsWithPaginationViewModel(new PagedCollection<PostItemViewModel>(postViewModels, posts.TotalItemsCount, pageSize, pageIndex)); 
            
            return View(viewModel);
        }

        private async Task<string> GenerateHtmlContent(PostData x)
        {
            return await _mediator.Send(new GenerateHtmlCommand(x.Content+"..."));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
