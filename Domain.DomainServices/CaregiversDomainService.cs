using Source.Core.DataService;
using Source.Core.DomainService;
using Source.Domains.Entities;

namespace Domain.DomainServices
{
    public class CaregiversDomainService : DomainService<Caregivers, long>
    {
        public CaregiversDomainService(IEntityDataService<Caregivers> entityDataService) : base(entityDataService)
        {
        }
    }
}