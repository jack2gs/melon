using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Melon.Wrap.Site.Models
{
    public class PagedCollection<T>
    {
        public PagedCollection(IEnumerable<T> results, 
            int totalCount, 
            int pageSize,
            int pageIndex)
        {
            Items = results;
            TotalCount = totalCount;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public IEnumerable<T> Items { get; }

        public int ItemsCount
        {
            get { return Items.Count(); }
        }

        public int TotalCount { get; }
        public int PageSize { get; }
        public int PageIndex { get; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((double)TotalCount / PageSize); }
        }

        public int PreviousPageIndex
        {
            get
            {
                if (ItemsCount < 0 ||PageIndex - 1 < 1)
                {
                    return -1;
                }

                return PageIndex - 1;
            }
        }

        public int NextPageIndex
        {
            get
            {
                if (ItemsCount < 0 || PageIndex + 1 > TotalPages)
                {
                    return -1;
                }
               
                return PageIndex + 1;
            }
        }
    }
}