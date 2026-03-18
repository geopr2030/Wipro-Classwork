using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
namespace DAL
{
    public class DriveEaseContext : DbContext
    {
        public DriveEaseContext(DbContextOptions<DriveEaseContext> options) : base(options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Lease> Leases { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}