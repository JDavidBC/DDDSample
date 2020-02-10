using Domain.DomainServices;
namespace Source.WebApi.Controllers
{
    using Source.Core.WebApi;
    using Domains.Entities;
    
    public class CaregiversController : WebApiControllerBase<Caregivers,long>
    {
        public CaregiversController(CaregiversDomainService caregiversDomainService):base(caregiversDomainService)
        {
            
        }
    }
}
