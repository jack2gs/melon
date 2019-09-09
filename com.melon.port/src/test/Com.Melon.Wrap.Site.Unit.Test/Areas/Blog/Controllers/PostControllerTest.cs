using Com.Melon.Core.Infrastructure;
using Com.Melon.Wrap.Site.Areas.Blog.Controllers;
using Com.Melon.Wrap.Site.Areas.Blog.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using XunitExtensions;

namespace Com.Melon.Wrap.Site.Unit.Test.Areas.Blog.Controllers
{
    public class PostControllerTestBase: Specification
    {
        protected PostController UnderTest;

        protected Mock<IMediator> MediactorMock;

        protected override void EstablishContext()
        {
            MediactorMock = new Mock<IMediator>();
            UnderTest = new PostController(MediactorMock.Object);
        }
    }

    public class When_get_creating_post_request: PostControllerTestBase
    {
        protected IActionResult ActualActionResult;

        protected DateTime ExpectedDateTime;

        protected override void EstablishContext()
        {
            ExpectedDateTime = DateTime.Now;

            Clock.FixNow(ExpectedDateTime);

            base.EstablishContext();
        }

        protected override void Because()
        {
           ActualActionResult = UnderTest.Create();
        }

        [Observation]
        void should_generate_the_view_action_result()
        {
            ActualActionResult.Should().BeOfType<ViewResult>();
        }

        [Observation]
        void should_have_empty_view_model()
        {
            var model = ((ActualActionResult as ViewResult).Model as PostViewModel);
            model.Should().BeOfType<PostViewModel>();
            model.Id.Should().Be(0);
            model.Title.Should().Be(string.Empty);
            model.Content.Should().Be(string.Empty);
            model.DateCreated.Should().Be(ExpectedDateTime);
            model.DateModified.Should().Be(ExpectedDateTime);
        }
    }
}
