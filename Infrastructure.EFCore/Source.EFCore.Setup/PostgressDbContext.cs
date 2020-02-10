using System;
using Microsoft.EntityFrameworkCore;
using Source.DataServices.EFCore.DataContext;
using Microsoft.Extensions.Configuration;

namespace Source.EFCore.Setup
{
    public class PostgressDbContext : AppDbContext
    {
        private readonly string _connectionString;
        
        public PostgressDbContext(IConfiguration config) : base(new DbContextOptionsBuilder<AppDbContext>().Options)
        {
            _connectionString = config.GetConnectionString("DefaultPostgresConnection");
            
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentNullException(nameof(_connectionString));

            
        } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}