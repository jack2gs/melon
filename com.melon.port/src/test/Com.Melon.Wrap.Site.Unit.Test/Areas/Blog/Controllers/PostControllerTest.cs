using Com.Melon.Blog.Application;
using Com.Melon.Core.Infrastructure;
using Com.Melon.Wrap.Site.Areas.Blog.Controllers;
using Com.Melon.Wrap.Site.Areas.Blog.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
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

    public class When_post_creating_post_request_base: PostControllerTestBase
    {
        protected IActionResult ActualActionResult;

        protected PostViewModel ViewModel;

        protected override void EstablishContext()
        {
            ViewModel = new PostViewModel("FakedTitle", "FakedContext");

            base.EstablishContext();
        }

        protected async override void Because()
        {
            UnderTest.ModelState.AddModelError("", "Faked Error");
            ActualActionResult = await UnderTest.Create(ViewModel);
        }
    }

    public class When_post_creating_post_request: When_post_creating_post_request_base
    {
        protected override void EstablishContext()
        {
            ViewModel = new PostViewModel("FakedTitle", "FakedContext");

            base.EstablishContext();
        }

        [Observation]
        void should_send_create_post_command()
        {
            MediactorMock.Verify(x => x.Send(It.Is<CreatePostCommand>(y => y.Title == ViewModel.Title && y.Content == ViewModel.Content), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Observation]
        void should_navigate_to_home_page()
        {
            RedirectToActionResult result = ActualActionResult as RedirectToActionResult;
            result.ActionName.Should().Be("Index");
            result.ControllerName.Should().Be("Home");
        }
    }

    public class When_post_creating_post_request_and_the_post_is_invalid : When_post_creating_post_request_base
    {
        protected ViewResult ActualViewResult;

        protected PostViewModel ActualViewModel;

        protected override void Because()
        {
            base.Because();
            ActualViewResult = ActualActionResult as ViewResult;
            ActualViewModel = ActualViewResult.Model as PostViewModel;
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();
            ViewModel = new PostViewModel("", "");
        }

        [Observation]
        void should_not_send_create_post_command()
        {
            MediactorMock.Verify(x => x.Send(It.Is<CreatePostCommand>(y => y.Title == ViewModel.Title && y.Content == ViewModel.Content), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Observation]
        void should_return_the_input_data()
        {
            ActualViewModel.Title.Should().Be(ViewModel.Title);
            ActualViewModel.Content.Should().Be(ViewModel.Content);
        }
    }
}
