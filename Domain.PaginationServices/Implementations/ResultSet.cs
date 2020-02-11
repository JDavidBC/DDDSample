using System.Collections.Generic;
using Domain.PaginationServices.Interfaces;

namespace Domain.PaginationServices.Implementations
{
    public partial class PageHelper
    {
        public class ResultSet<TType> : IResultSet<TType>
        {
            public IEnumerable<TType> Items { get; set; }
            public Pager Pager { get; set; }
        }
    }
}