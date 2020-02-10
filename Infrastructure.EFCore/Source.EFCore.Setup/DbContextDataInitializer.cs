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
            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            // Look for any data available.
            if (context.Caregivers.Any())
            {
                return; // DB has been seeded
            }

            for (int i = 0; i < 10; i++)
                context.Caregivers.Add(
                    EntityDataFactory<Caregivers>.Factory_Entity_Instance( 
                        x =>
                        {
                            x.CaregiverId = 0;
                            x.Nif = "12345678";
                            x.Password = "password";
                            x.BirthDate = Convert.ToDateTime("2000-01-01");
                            x.Name = "Nombre";
                            x.FirstSurname = "Firstsurname";
                            x.SecondSurname = "SecondSurname";
                            x.Alias = "Popeye el marino";
                            x.StreetHome = "Calle de popeye";
                            x.NumberHome = "1";
                            x.PortalHome = 1;
                            x.LetterHome = "A";
                            x.CountryHomeId = -1;
                            x.ProvinceHomeId = -1;
                            x.TownHomeId = -1;
                            x.Email = "popeye@marinero.soy";
                            x.Phone = "asdad";
                            x.Imei = "AASDASDAAS23423425ADS";
                            x.EntryDate = DateTime.Now;
                            x.Active = true;
                        }));

            context.SaveChanges();
        }
    }
}