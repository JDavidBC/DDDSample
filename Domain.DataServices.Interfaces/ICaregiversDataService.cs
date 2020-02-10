using System.Collections.Generic;
using System.Threading.Tasks;
using Source.Core.DataService;
using Source.Domains.Entities;

namespace Domain.DataServices.Interfaces
{
    public interface ICaregiversDataService: IEntityDataService<Caregivers>
    {
        Task<IList<Caregivers>> GetByFirstName(string firstName);

    }
}