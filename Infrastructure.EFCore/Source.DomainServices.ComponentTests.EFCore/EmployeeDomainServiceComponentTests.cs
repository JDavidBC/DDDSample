using Source.DataServices.EFCore;
using Source.Domains.Entities;
using Source.EFCore.Setup;
using Source.Test.Core.TestBases;

namespace Source.DomainServices.ComponentTests.EFCore
{
    public class EmployeeDomainServiceComponentTests : DomainServiceBaseComponentTests<Employee, int>
    {
        public EmployeeDomainServiceComponentTests() :
            base(new EmployeeDomainService(Factory_DataService()), x => x.Id)
        {
            
        }

        static EmployeeDataService Factory_DataService()
        {
            EmployeeDataService employeeDataService = new EmployeeDataService(TestDbContextFactory.CreateDbContext());

            return employeeDataService;
        }
    }
}
