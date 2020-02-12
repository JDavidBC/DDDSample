using Microsoft.AspNetCore.Http;
using Source.Pagination.Dto;
using Source.Pagination.Implementations;

namespace Source.Core.Utils
{
    public class ResponsePagination
    {
        public static void AddResponsePagination<T>(HttpContext context, Envelope<T> pagination) where T: class
        {
            context.Response.AddPagination(pagination.Meta.Links.Pager.CurrentPage, 
                pagination.Meta.Links.Pager.PageSize, 
                pagination.Meta.Links.Pager.TotalRecords, 
                pagination.Meta.Links.Pager.NumberOfPages);
        }

        public static PaginationDto GetPaginationDto(HttpContext context, HttpRequest request)
        {
            
            var dto = new PaginationDto();
            
            var query = request.QueryString;
            
            if (!string.IsNullOrEmpty(query.ToString()))
            {
                var page =  request.Query["page"].ToString();
                var pageSize = request.Query["pageSize"].ToString();

                int ipage;
                int ipageSize;

                var isNUmericPage = int.TryParse(page, out ipage);
                var isNumericPageSize = int.TryParse(pageSize, out ipageSize);
                
                if(isNUmericPage && isNumericPageSize)
                    dto = new PaginationDto( ipage, ipageSize);
                else
                {
                    context.Response.AddAplicationError("ERROR IN PAGINATION QUERY");
                    
                }
            }

            return dto;
        }

    }
}