using It_AcademyHomework.JwtExample.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace It_AcademyHomework.JwtExample.Repository
{
    public class CustomersDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
