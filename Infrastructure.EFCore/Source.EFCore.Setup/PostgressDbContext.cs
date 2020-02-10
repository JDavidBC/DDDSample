using System;
using Microsoft.EntityFrameworkCore;
using Source.DataServices.EFCore.DataContext;
using Microsoft.Extensions.Configuration;

namespace Source.EFCore.Setup
{
    public class PostgressDbContext : AppDbContext
    {
        public PostgressDbContext(IConfiguration configuration) : this(configuration.GetConnectionString("DefaultPostgressConnection")) { }

        private readonly string _connectionString;
        
        public PostgressDbContext(string connectionString = null) : base(new DbContextOptionsBuilder<AppDbContext>().Options)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}