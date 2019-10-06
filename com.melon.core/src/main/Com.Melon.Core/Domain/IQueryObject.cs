using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Com.Melon.Core.Domain
{
    public interface IQueryObject<TAggregateRoot>
        where TAggregateRoot: IAggregateRoot
    {
        Expression<Func<TAggregateRoot, bool>> GetPredicate();

//        IEnumerable<OrderBy> GetOrderBy();
//
//        Pagination GetPagination();
    }
}
