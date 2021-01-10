using ClientesWebAPI.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesWebAPI
{
    public class ZupContext : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Telefone> Telefone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
            .HasMany(b => b.Telefones)
            .WithOne();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = "TesteZUP",
                UserID = "sa",
                Password = "saadmin"
            };

            var connectionString = builder.ConnectionString;

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
