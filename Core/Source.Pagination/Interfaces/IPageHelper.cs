using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Source.Pagination.Dto;
using Source.Pagination.Implementations;

namespace Source.Pagination.Interfaces
{
    public interface IPageHelper
    {
        /// <summary>
        /// Returns the data of the specified page
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="items">The IQueryable of the dataSource</param>
        /// <param name="paginationDto">Number of the page and page size</param>
        /// <returns>The current page of the data</returns>
        Task<Envelope<IEnumerable<T>>> GetPageAsync<T>(IQueryable<T> items, PaginationDto paginationDto) where T : class;
        
        Task<Envelope<IEnumerable<T>>> GetPage<T>(IEnumerable<T> items, PaginationDto paginationDto) where T : class;

        /// <summary>
        /// Projects <paramref name="items"/> into the specified type and
        /// Returns the data of the specified page
        /// </summary>
        /// <typeparam name="TSource">Type of the source data</typeparam>
        /// <typeparam name="TTarget">Type of the data after projection</typeparam>
        /// <param name="items">The IQueryable of the dataSource</param>
        /// <param name="paginationDto">Number of the page and page size</param>
        /// <returns>The current page of the data</returns>
        Task<Envelope<IEnumerable<TTarget>>> GetProjectedPageAsync<TSource, TTarget>(IQueryable<TSource> items, PaginationDto paginationDto)
            where TSource : class
            where TTarget : class;

        /// <summary>
        /// Generates pagination links
        /// </summary>
        /// <param name="pager">The pagination page stats data</param>
        /// <returns>Pagination data</returns>
        Pagination GetPagination(Pager pager);

    }
}