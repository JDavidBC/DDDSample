using System.Collections.Generic;

namespace Source.Pagination.Interfaces
{
    /// <summary>
    /// Pagination result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResultSet<T>
    {
        /// <summary>
        /// Items in the page
        /// </summary>
        IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Pager data
        /// </summary>
        Pager Pager { get; set; }
    }

}