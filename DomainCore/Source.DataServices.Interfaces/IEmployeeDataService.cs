using System.Collections.Generic;
using Source.Core.DataService;
using System.Threading.Tasks;
using Source.Domains.Entities;

namespace Source.DataServices.Interfaces
{
    public interface IEmployeeDataService: IEntityDataService<Employee>
    {
        Task<IList<Employee>> GetByFirstName(string firstName);

    }
}