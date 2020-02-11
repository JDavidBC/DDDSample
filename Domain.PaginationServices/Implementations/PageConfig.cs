using System;
using Domain.PaginationServices.Interfaces;

namespace Domain.PaginationServices.Implementations
{
    public class PageConfig : IPageConfig
    {
        public PageConfig()
        {

        }
        public PageConfig(int pageSize)
        {
            if (pageSize < 1)
            {
                throw new ArgumentException("pageSize cannot be less than 1", nameof(pageSize));
            }
            PageSize = pageSize;
        }

        public int PageSize { get; } = 10;
    }
}