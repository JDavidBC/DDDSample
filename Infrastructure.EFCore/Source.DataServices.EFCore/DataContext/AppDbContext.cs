using Microsoft.EntityFrameworkCore;

namespace Source.DataServices.EFCore.DataContext
{
    using Domains.Entities;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Caregivers> Caregivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caregivers>(entity => { entity.ToTable("Caregivers"); });


        }
    }
}