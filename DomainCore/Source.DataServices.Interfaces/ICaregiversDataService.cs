using System.Collections.Generic;
using Source.Core.DataService;
using System.Threading.Tasks;
using Source.Domains.Entities;

namespace Source.DataServices.Interfaces
{
    public interface ICaregiversDataService: IEntityDataService<Caregivers>
    {
        Task<IList<Caregivers>> GetByFirstName(string firstName);

    }
}