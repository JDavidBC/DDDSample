using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Source.Core.DomainService;
using Microsoft.AspNetCore.Mvc;
using Source.Pagination.Dto;
using Source.Core.Utils;

namespace Source.Core.WebApi
{
    [Route("api/[controller]")]
    public abstract class WebApiControllerBase<TDomain,TId>: Controller
        where TDomain : class, new()
    {
        protected DomainService<TDomain,TId> DomainService;
        protected WebApiControllerBase(DomainService<TDomain, TId> domainService)
        {
            DomainService = domainService;
        }

        [HttpGet]
        public virtual async Task<IEnumerable<TDomain>> Get()
        {
            return await DomainService.GetAll();
        }
        
        [HttpGet("getpaginate/")]
        public virtual async Task<IEnumerable<TDomain>> GetPaginate([FromQuery] PaginationDto paginationDto)
        {
            var pagination =  await DomainService.GetPaginate(paginationDto);                    
                    

            Response.AddPagination(pagination.Meta.Links.Pager.CurrentPage, 
                pagination.Meta.Links.Pager.PageSize, 
                pagination.Meta.Links.Pager.TotalRecords, 
                pagination.Meta.Links.Pager.NumberOfPages);


            return pagination.Data;

        }

        [HttpGet("{id}")]
        public virtual async Task<TDomain> Get(TId id)
        {
            return await DomainService.GetById(id);
        }

        [HttpPost]
        public virtual async Task Post(TDomain domain)
        {
            await DomainService.Add(domain);
            
            
        }

        [HttpPut("{id}")]
        public virtual async Task Put(TId id, TDomain domain)
        {
            await DomainService.Update(id, domain);
        }

        [HttpDelete("{id}")]
        public virtual async Task Delete(TId id)
        {
            await DomainService.Delete(id);
        }
    }
}
