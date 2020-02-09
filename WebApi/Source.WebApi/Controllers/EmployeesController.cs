namespace Source.WebApi.Controllers
{
    using Source.Core.WebApi;
    using Domains.Entities;
    using DomainServices;

    public class EmployeesController : WebApiControllerBase<Employee,int>
    {
        public EmployeesController(EmployeeDomainService employeeDomainService):base(employeeDomainService)
        {
            
        }
    }
}
