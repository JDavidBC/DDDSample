using System.Linq;

namespace Source.EFCore.Setup
{
    using Source.DataServices.EFCore.DataContext;
    using Domains.Entities;
    using Source.Test.Core.DataGen;

    public static class DbContextDataInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            // Look for any data available.
            if (context.Employees.Any())
            {
                return; // DB has been seeded
            }

            for (int i = 0; i < 10; i++)
                context.Employees.Add(
                    EntityDataFactory<Employee>.Factory_Entity_Instance( 
                        x =>
                        {
                            x.Id = 0;
                            x.ReportsToId = null;
                        }));

            context.SaveChanges();
        }
    }
}