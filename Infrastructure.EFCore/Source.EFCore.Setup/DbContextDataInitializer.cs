using System;
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
           // context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            // Look for any data available.
            if (context.Caregivers.Any())
            {
                return; // DB has been seeded
            }

            for (int i = 0; i < 10; i++)
            {
                var emp= EntityDataFactory<Caregivers>.Factory_Entity_Instance(
                    x =>
                    {
                        x.CaregiverId = 0;
                        x.ProvinceHomeId = -1;
                        x.CountryHomeId = -1;
                        x.TownHomeId = -1;
                        x.Active = true;
                    });
                
                context.Caregivers.Add(emp);
            }
            
            
            

            context.SaveChanges();
        }
    }
}