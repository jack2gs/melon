using Com.Melon.Blog.Application;
using Com.Melon.Core.Infrastructure;
using Com.Melon.Wrap.Site.Areas.Blog.Controllers;
using Com.Melon.Wrap.Site.Areas.Blog.Models;
using Com.Melon.Wrap.Site.Core.Application;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
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
            UnderTest.ModelState.AddModelError("", "Faked Error");
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

    public class When_get_edit_post_request_base : PostControllerTestBase
    {
        protected IActionResult ActualActionResult;

        protected PostViewModel ExpectedPostViewModel;

        protected int ExpectedPostId;

        protected async override void Because()
        {
            ActualActionResult = await UnderTest.Edit(1);
        }
    }

    public class When_get_post_and_post_exist: When_get_edit_post_request_base
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();

            ExpectedPostId = 1;
            ExpectedPostViewModel = new PostViewModel(ExpectedPostId, "FakedTitle", "FakedContent", Clock.Now, Clock.Now);

            MediactorMock.Setup(x => x.Send(It.Is<PostQuery>(y => y.PostId == ExpectedPostId), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new PostData(ExpectedPostViewModel.Id, ExpectedPostViewModel.Title, ExpectedPostViewModel.Content, ExpectedPostViewModel.DateCreated, ExpectedPostViewModel.DateModified)));
        }

        [Observation]
        void should_return_post_view()
        {
            ViewResult result = ActualActionResult as ViewResult;

            ShouldEqual(result.Model as PostViewModel, ExpectedPostViewModel);
        }

        void ShouldEqual(PostViewModel actualModel, PostViewModel expectedModel)
        {
            actualModel.Id.Should().Be(expectedModel.Id);
            actualModel.Title.Should().Be(expectedModel.Title);
            actualModel.Content.Should().Be(expectedModel.Content);
        }
    }

    public class When_get_post_and_post_not_exist : When_get_edit_post_request_base
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();

            ExpectedPostId = 1;

            MediactorMock.Setup(x => x.Send(It.Is<PostQuery>(y => y.PostId == ExpectedPostId), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<PostData>(null));
        }

        [Observation]
        void should_navigate_to_home_page()
        {
            RedirectToActionResult result = ActualActionResult as RedirectToActionResult;
            result.ActionName.Should().Be("Index");
            result.ControllerName.Should().Be("Home");
        }
    }

    public class When_post_edit_post_request_base : PostControllerTestBase
    {
        protected IActionResult ActualActionResult;

        protected PostViewModel ExpectedPostViewModel;

        protected PostViewModel PostViewModel;

        protected async override void Because()
        {
            ActualActionResult = await UnderTest.Edit(PostViewModel);
        }
    }

    public class When_post_edit_post_request_and_post_exists : When_post_edit_post_request_base
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            PostViewModel = new PostViewModel(1, "mytitle", "mycontent");
        }

        [Observation]
        void should_call_update_post_command()
        {
            MediactorMock.Verify(x => x.Send(It.Is<UpdatePostCommand>(y => y.PostId == PostViewModel.Id && y.Title == PostViewModel.Title && y.Content == PostViewModel.Content), It.IsAny<CancellationToken>()), Times.Once);
        }

        void should_navigate_to_home_page()
        {
            ActualActionResult.Should().BeOfType<RedirectToActionResult>();
        }
    }

    public class When_post_edit_post_request_and_throws_argument_exception : When_post_edit_post_request_base
    {
        protected string ExpectedErrorMessage;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            PostViewModel = new PostViewModel(1, "mytitle", "mycontent");
            ExpectedPostViewModel = new PostViewModel(1, "mytitle", "mycontent");
            ExpectedErrorMessage = "Post not found";
            MediactorMock.Setup(x => x.Send(It.Is<UpdatePostCommand>(y => y.PostId == PostViewModel.Id && y.Title == PostViewModel.Title && y.Content == PostViewModel.Content), It.IsAny<CancellationToken>())).Throws(new ArgumentException(ExpectedErrorMessage));
        }

        [Observation]
        void should_call_update_post_command()
        {
            MediactorMock.Verify(x => x.Send(It.Is<UpdatePostCommand>(y => y.PostId == PostViewModel.Id && y.Title == PostViewModel.Title && y.Content == PostViewModel.Content), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Observation]
        void should_has_error_message()
        {
            ViewResult result = ActualActionResult as ViewResult;
            PostViewModel model = result.Model as PostViewModel;

            Equals(model, ExpectedPostViewModel).Should().BeTrue();
            UnderTest.ModelState[string.Empty].Errors.First().ErrorMessage.Should().Be(ExpectedErrorMessage);
        }

        bool Equals(PostViewModel actual, PostViewModel expected)
        {
            return actual.Id == expected.Id && actual.Title == expected.Title && actual.Content == expected.Content;
        }
    }

    public class When_view_post_base : PostControllerTestBase
    {
        protected PostQuery PostQuery { get; set; }

        protected PostData PostData { get; set;}

        protected string HtmlContent { get; set; }

        protected HtmlPostViewModel ExpectedHtmlPostViewModel { get; set; }

        protected IActionResult ActualActionResult;

        protected Exception ActualException { get; set; }

        protected override void EstablishContext()
        {
            base.EstablishContext();
            PostQuery = CreatePostQuery(1);
            PostData = CreatePostData();
            ExpectedHtmlPostViewModel = CreateHtmlPostViewModel();
            HtmlContent = GetHtmlContent();

            MediactorMock.Setup(x => x.Send(It.Is<PostQuery>(y => y.PostId == PostQuery.PostId), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<PostData>(PostData));
            MediactorMock.Setup(x => x.Send(It.Is<GenerateHtmlCommand>(y => y.Markdown == PostData.Content), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<string>(HtmlContent));
        }

        protected virtual string GetHtmlContent()
        {
            return "Content";
        }

        protected virtual HtmlPostViewModel CreateHtmlPostViewModel()
        {
            return new HtmlPostViewModel(1, "Title", "Content");
        }

        protected virtual PostData CreatePostData()
        {
            return new PostData(1, "Title", "Content", Clock.Now, Clock.Now);
        }

        protected virtual PostQuery CreatePostQuery(int postId)
        {
            return new PostQuery(postId);
        }

        protected async override void Because()
        {
            ActualException = await Record.ExceptionAsync(async () => ActualActionResult = await UnderTest.Detail(PostQuery.PostId));
        }
    }

    public class When_view_post_post_and_it_exists: When_view_post_base
    {
        [Observation]
        void should_get_the_markdown()
        {
            MediactorMock.Verify((x) => x.Send(It.Is<PostQuery>(y => y.PostId == PostQuery.PostId)
                , It.IsAny<CancellationToken>()), Times.Once);
        }

        [Observation]
        void should_convert_to_html()
        {
            MediactorMock.Verify((x) => x.Send(It.Is<GenerateHtmlCommand>(y => y.Markdown == PostData.Content)
                           , It.IsAny<CancellationToken>()), Times.Once);
        }

        [Observation]
        void should_return_post()
        {
            ViewResult result = ActualActionResult as ViewResult;

            HtmlPostViewModel model = result.Model as HtmlPostViewModel;
            model.PostId.Should().Be(ExpectedHtmlPostViewModel.PostId);
            model.Title.Should().Be(ExpectedHtmlPostViewModel.Title);
            model.HtmlContent.Should().Be(ExpectedHtmlPostViewModel.HtmlContent);
        }
    }

    public class When_view_post_post_and_it_does_not_exists : When_view_post_base
    {
        protected override PostData CreatePostData()
        {
            return null;
        }

        protected override HtmlPostViewModel CreateHtmlPostViewModel()
        {
            return null;
        }

        [Observation]
        void should_get_the_markdown()
        {
            MediactorMock.Verify((x) => x.Send(It.Is<PostQuery>(y => y.PostId == PostQuery.PostId)
                , It.IsAny<CancellationToken>()), Times.Once);
        }

        [Observation]
        void should_not_convert_to_html()
        {
            MediactorMock.Verify((x) => x.Send(It.IsAny<GenerateHtmlCommand>()
                           , It.IsAny<CancellationToken>()), Times.Never);
        }

        [Observation]
        void should_navigate_to_home()
        {
            RedirectToActionResult result = ActualActionResult as RedirectToActionResult;

            result.ActionName.Should().Be("Index");
            result.ControllerName.Should().Be("Home");
        }
    }
}
