using Com.Melon.Blog.Domain;
using FluentAssertions;
using System;
using Com.Melon.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
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

        protected DateTime FixedDateTimeNow;

        protected override void EstablishContext()
        {
            Title = string.Empty;
            Content = "FakedContent";
            FixedDateTimeNow = DateTime.Now;
            Clock.FixNow(FixedDateTimeNow);
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() => ActualPost = new Post(Title, Content));
        }

        protected override void DestroyContext()
        {
            Clock.Resume();
        }
    }
    
    [Collection("Post Collection #1")]
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

    [Collection("Post Collection #1")]
    public class When_title_is_valid : PostTestBase
    {
        protected override void EstablishContext()
        {
            Title = "FackedTitle";
            Content = "FackedContent";
            FixedDateTimeNow = DateTime.Now;
            Clock.FixNow(FixedDateTimeNow);
        }

        protected override void DestroyContext()
        {
            Clock.Resume();
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

        [Observation]
        void should_have_datetime_stamp()
        {
            ActualPost.DateTimeCreated.Should().Be(FixedDateTimeNow);
            ActualPost.DateTimeLastModified.Should().Be(FixedDateTimeNow);
        }
    }

    [Collection("Post Collection #1")]
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

    [Collection("Post Collection #1")]
    public class When_excerpt_content_without_excerpt_flag : PostTestBase
    {
        protected string ActualContent;

        protected string ExceptedContent;
        
        protected override void EstablishContext()
        {
            Title = "MyTile";
            Content = "FakedContent";
            ExceptedContent = "FakedContent";
        }

        protected override void Because()
        {
            base.Because();
            ActualContent = ActualPost.ExcerptContent();
        }

        [Observation]
        void should_excerpt_content()
        {
            ActualContent.Should().Be(ExceptedContent);
        }
    }
    
    [Collection("Post Collection #1")]
    public class When_excerpt_content_with_excerpt_flag : PostTestBase
    {
        protected string ActualContent;

        protected string ExceptedContent;
        
        protected override void EstablishContext()
        {
            Title = "MyTile";
            Content = "FakedContent<!--more-->test1";
            ExceptedContent = "FakedContent";
        }

        protected override void Because()
        {
            base.Because();
            ActualContent = ActualPost.ExcerptContent();
        }

        [Observation]
        void should_excerpt_content()
        {
            ActualContent.Should().Be(ExceptedContent);
        }
    }
    
    [Collection("Post Collection #1")]
    public class When_update_post : PostTestBase
    {
        protected string UpdatedContent;

        protected string UpdatedTitle;

        protected override void EstablishContext()
        {
            Title = "MyTile";
            Content = "FakedContent<!--more-->test1";
            ActualPost = new Post(Title, Content);

            UpdatedTitle = "UpdatedTitle";
            UpdatedContent = "UpdatedContent";
            
            FixedDateTimeNow = DateTime.Now; 
            Clock.FixNow(FixedDateTimeNow);
        }

        protected override void Because()
        {
            ActualPost.Update(UpdatedTitle, UpdatedContent);
        }

        [Observation]
        void should_update_title()
        {
            ActualPost.Title.Should().Be(UpdatedTitle);
        }
        
        [Observation]
        void should_update_content()
        {
            ActualPost.Content.Should().Be(UpdatedContent);
        }

        [Observation]
        void should_update_timestamp()
        {
            ActualPost.DateTimeLastModified.Should().Be(FixedDateTimeNow);
        }

        protected override void DestroyContext()
        {
            Clock.Resume();
        }
    }
}
