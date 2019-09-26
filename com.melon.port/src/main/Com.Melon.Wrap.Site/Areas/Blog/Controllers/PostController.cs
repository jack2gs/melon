using System;
using System.Threading;
using System.Threading.Tasks;
using Com.Melon.Blog.Application;
using Com.Melon.Core.Infrastructure;
using Com.Melon.Wrap.Site.Areas.Blog.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Com.Melon.Wrap.Site.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class PostController : Controller
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Create()
        {
            DateTime now = Clock.Now;
            return View(new PostViewModel(string.Empty, string.Empty, now, now));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                CreatePostCommand command = new CreatePostCommand(postViewModel.Title, postViewModel.Content);
                await _mediator.Send(command);
                return RedirectToAction("Index", "Home");
            }

            return View(postViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _mediator.Send(new PostQuery(id), default(CancellationToken));

            if (post == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new PostViewModel(post.PostId, post.Title, post.Content));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostViewModel postViewModel)
        {
            try
            {
                await _mediator.Send(new UpdatePostCommand(postViewModel.Id, postViewModel.Title, postViewModel.Content));
            }
            catch(ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(postViewModel);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}