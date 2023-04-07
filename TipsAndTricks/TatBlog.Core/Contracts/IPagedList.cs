using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.Core.Contracts;

public interface IPagedList
{
    int PagedCount { get; }

    int TotalItemCount { get; }

    int PageIndex { get; }

    int PageNumber { get; }

    int PageSize { get; }

    bool HasPreviosPage { get; }

    bool HasNextPage { get; }

    bool IsFirstPage { get; }

    bool IsLastPage { get; }

    int FirstItemIndex { get; }

    int LastItemIndex { get; }
}

public interface IPagedList<out T> : IPagedList, IEnumerable<T>
{
    T this[int index] { get; }

    int Count { get; }
}
