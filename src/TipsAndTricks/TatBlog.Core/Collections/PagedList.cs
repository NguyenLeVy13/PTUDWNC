using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;

namespace TatBlog.Core.Collections
{
    public class PagedList<T> : IPagedList<T>
    {
        private readonly List<T> _subset = new();

        public PagedList(
            IList<T> items,
            int pageNumber,
            int pagrSize,
            int totalCount)
        {
            PageNumber = pageNumber;
            PageSize = pagrSize;
            TotalItemCount = totalCount;

            _subset.AddRange(items);
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalItemCount { get; set; }

        public int PageNumber
        {
            get => PageIndex + 1;
            set => PageIndex = value - 1;
        }


        public int PagedCount
        {
            get
            {
                if (PageSize == 0)
                    return 0;

                var total = TotalItemCount / PageSize;

                if (TotalItemCount % PageSize > 0)
                    total++;
                return total;
            }
        }


        public bool HasNextPage => (PageIndex < (PagedCount - 1));

        public int FirstItemIndex => (PageIndex * PageSize) + 1;

        public int LastItemIndex
            => Math.Min(TotalItemCount, ((PageIndex * PageSize) + PageSize));

        public bool IsFirstPage => (PageIndex <= 0);

        public bool IsLastPage => (PageIndex >= (PagedCount - 1));

        #region IPagedList<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return _subset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[int index] => _subset[index];

        public virtual int Count => _subset.Count;


        public bool HasPreviosPage => PageIndex > 0;

        #endregion
    }
}
