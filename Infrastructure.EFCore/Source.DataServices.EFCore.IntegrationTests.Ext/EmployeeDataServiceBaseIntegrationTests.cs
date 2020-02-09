using Source.DataServices.Interfaces;
using Source.Domains.Entities;
using Source.Test.Core.TestBases;

namespace Source.DataServices.EFCore.IntegrationTests.Ext
{
    public abstract class EmployeeDataServiceBaseIntegrationTests : DataServiceBaseIntegrationTests<Employee, int>
    {
        protected EmployeeDataServiceBaseIntegrationTests(IEmployeeDataService employeeDataService) :base (employeeDataService, x => x.Id)
        {

        }

    }
}
