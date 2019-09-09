using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Melon.Wrap.Site.Areas.Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Com.Melon.Wrap.Site.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class PostController : Controller
    {
        public IActionResult Create()
        {
            DateTime now = DateTime.Now;
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