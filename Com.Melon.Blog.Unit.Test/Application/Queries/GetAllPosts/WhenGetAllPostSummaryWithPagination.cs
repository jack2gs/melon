using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Com.Melon.Blog.Application.Queries.GetAllPosts;
using Com.Melon.Blog.Domain;
using Com.Melon.Core.Framework.Pagination;
using FluentAssertions;
using Moq;
using Xunit;
using XunitExtensions;

namespace Com.Melon.Blog.Unit.Test.Application.Queries.GetAllPosts
{
    public class WhenGetAllPostSummaryWithPagination: Specification
    {
        [Observation]
        async void should_return_post_summary_with_pagination()
        {
            IPostRepository postRepository = Mock.Of<IPostRepository>();
            GetAllPostSummaryWithPaginationQueryHandler handler = new GetAllPostSummaryWithPaginationQueryHandler(postRepository);
            GetAllPostSummaryWithPaginationQuery query = new GetAllPostSummaryWithPaginationQuery(1, 20);

            Mock.Get(postRepository).Setup(x => x.GetAllAsync(1, 20, It.IsAny<CancellationToken>())).ReturnsAsync(() =>
                new PagedResult<Post>(new List<Post>() {
                        new Post(1, "Title", "Content1<!--more-->test2"),
                        new Post(2, "Title", "Content2<!--more-->test2")
                    }, 
                    100));

            var posts = await handler.Handle(query, default(CancellationToken));

            posts.Items.Count().Should().Be(2);
            posts.TotalItemsCount.Should().Be(100);
            
            var postItems = posts.Items.ToList();
            postItems[0].Content.Should().Be("Content1");
            postItems[1].Content.Should().Be("Content2");
        }
    }
}