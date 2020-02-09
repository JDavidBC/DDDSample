namespace Source.DataServices.EFCore.IntegrationTests.Ext
{
    using Source.EFCore.Setup;

    public class EmployeeDataServiceTests: EmployeeDataServiceBaseIntegrationTests
    {
        public EmployeeDataServiceTests():base (new EmployeeDataService(TestDbContextFactory.CreateDbContext()))
        {

        }

    }
}
