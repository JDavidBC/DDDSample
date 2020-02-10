using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Source.DataServices.EFCore.DataContext
{
    using Domains.Entities;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<Employee>().OwnsOne(c => c.Address);
           modelBuilder.Entity<Employee>(entity =>
           {
               entity.ToTable("Employees");

               entity.HasIndex(e => e.Id)
                   .HasName("Id")
                   .IsUnique();
               
               //entity.OwnsOne(c => c.Address);
           });

           
        }
    }
}