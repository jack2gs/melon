using Com.Melon.Blog.Domain;
using FluentAssertions;
using System;
using Xunit;
using XunitExtensions;

namespace Com.Melon.Blog.Unit.Test.Domain
{
    public class PostTestBase: Specification
    {
        protected string Title;

        protected string Content;

        protected Exception ActualException;

        protected Post ActualPost;

        protected override void EstablishContext()
        {
            Title = string.Empty;
            Content = "FakedContent";
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() => ActualPost = new Post(Title, Content));
        }
    }

    public class When_title_is_empty: PostTestBase
    {
        [Observation]
        void should_not_create_post()
        {
            ActualPost.Should().BeNull();
        }

        [Observation]
        void should_throw_argument_exception()
        {
            ActualException.Should().BeOfType<ArgumentException>();
        }
    }

    public class When_title_is_valid : PostTestBase
    {
        protected override void EstablishContext()
        {
            Title = "FackedTitle";
            Content = "FackedContent";
        }

        [Observation]
        void should_create_post()
        {
            ActualPost.Should().NotBeNull();
        }

        [Observation]
        void should_has_the_same_title()
        {
            ActualPost.Title.Should().Be(Title);
        }

        [Observation]
        void should_has_the_same_content()
        {
            ActualPost.Content.Should().Be(Content);
        }

        [Observation]
        void should_not_throw_argument_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_title_contains_invalid_char: PostTestBase
    {
        protected override void EstablishContext()
        {
            Title = "-&^&";
            Content = "FakedContent";
        }

        [Observation]
        void should_throw_argument_exception()
        {
            ActualException.Should().BeOfType<ArgumentException>();
            ActualException.Message.Should().Be("The title should just contains characters, numbers or underscore.");
        }
    }
}
