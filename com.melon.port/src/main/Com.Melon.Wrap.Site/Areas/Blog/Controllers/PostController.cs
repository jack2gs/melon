using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Create()
        {
            DateTime now = Clock.Now;
            return View(new PostViewModel(string.Empty, string.Empty, now, now));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostViewModel postViewModel)
        {
            return View(postViewModel);
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PostViewModel postViewModel)
        {
            return View(postViewModel);
        }
    }
}