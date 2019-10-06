using System.Collections.Generic;
using System.Linq;

namespace Com.Melon.Core.Framework.Pagination
{
    public class PagedResult<TElement>
        where TElement:class
    {
        public IEnumerable<TElement> Items { get; }
        
        public int TotalItemsCount { get; }

        public int ItemsCount => Items.Count();

        public PagedResult(IEnumerable<TElement> results, int totalCount)
        {
            Items = results;
            TotalItemsCount = totalCount;
        }
    }
}