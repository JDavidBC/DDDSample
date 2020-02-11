namespace Domain.PaginationServices.Interfaces
{
    public interface IPageConfig
    {
        /// <summary>
        /// Default page size of an envelope
        /// </summary>
        int PageSize { get; }
    }
}