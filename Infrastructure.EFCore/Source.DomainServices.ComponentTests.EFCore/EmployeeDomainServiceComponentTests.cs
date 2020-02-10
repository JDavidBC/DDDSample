using Domain.DomainServices;
using Source.DataServices.EFCore;
using Source.Domains.Entities;
using Source.EFCore.Setup;
using Source.Test.Core.TestBases;

namespace Source.DomainServices.ComponentTests.EFCore
{
    public class CaregiversDomainServiceComponentTests : DomainServiceBaseComponentTests<Caregivers, long>
    {
        public CaregiversDomainServiceComponentTests() :
            base(new CaregiversDomainService(Factory_DataService()), x => x.CaregiverId)
        {
            
        }

        static CaregiversDataService Factory_DataService()
        {
            var caregiversDataService = new CaregiversDataService(TestDbContextFactory.CreateDbContext());

            return caregiversDataService;
        }
    }
}
