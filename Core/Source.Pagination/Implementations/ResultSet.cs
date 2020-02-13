using System.Collections.Generic;
using Source.Pagination.Interfaces;

namespace Source.Pagination.Implementations
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