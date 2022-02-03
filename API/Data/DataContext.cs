using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Warehouse>? Warehouses{get;set;}
        public DbSet<Vehicle>? Vehicles{get;set;}
    }
}