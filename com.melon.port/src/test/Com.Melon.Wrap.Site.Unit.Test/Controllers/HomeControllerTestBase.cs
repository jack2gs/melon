using System.Collections.Generic;
using System.Threading;
using Com.Melon.Blog.Application;
using Com.Melon.Blog.Application.Queries.GetAllPosts;
using Com.Melon.Core.Framework.Pagination;
using Com.Melon.Core.Infrastructure;
using Com.Melon.Wrap.Site.Controllers;
using Com.Melon.Wrap.Site.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using XunitExtensions;

namespace Com.Melon.Wrap.Site.Unit.Test.Controllers
{
    public class HomeControllerTestBase: Specification
    {
        protected HomeController HomeController;
        protected IMediator Mediator;
        
        protected override void EstablishContext()
        {
            Mediator = Mock.Of<IMediator>();
            HomeController = new HomeController(Mediator);
        }
    }

    public class When_view_home_page : HomeControllerTestBase
    {
        protected IActionResult ActualResult;
        
        protected const int PageIndex = 1;

        protected const int PageSize = 20;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            Mock.Get(Mediator)
                .Setup(x => x.Send(
                    It.Is<GetAllPostSummaryWithPaginationQuery>(y => y.PageIndex == PageIndex && y.PageSize == PageSize),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(()=>new PagedResult<PostData>(new List<PostData>()
                {
                    new PostData(1, "fakedTitle", "fakedContent", Clock.Now, Clock.Now)
                }, 100));
        }

        protected override async void Because()
        {
            ActualResult = await HomeController.Index(PageIndex, PageSize);
        }

        [Observation]
        void should_call_mediator()
        {
            Mock.Get(Mediator).Verify(x=>x.Send(It.Is<GetAllPostSummaryWithPaginationQuery>(y=>y.PageIndex==PageIndex&&y.PageSize==PageSize),It.IsAny<CancellationToken>()), Times.Once);
        }

        [Observation]
        void should_return_posts()
        {
            var result = ActualResult as ViewResult;
            var posts = (result.Model as AllPostsWithPaginationViewModel).Posts;

            posts.ItemsCount.Should().Be(1);
            posts.TotalCount.Should().Be(100);
            posts.PageSize.Should().Be(PageSize);
            posts.PageIndex.Should().Be(PageIndex);
            posts.TotalPages.Should().Be(5);
            posts.PreviousPageIndex.Should().Be(-1);
            posts.NextPageIndex.Should().Be(2);
        }
    }
}