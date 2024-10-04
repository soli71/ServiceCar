using Microsoft.EntityFrameworkCore;

namespace OilChangeApp.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<Car> Cars { get; set; }
    }

    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
    }

    public class CustomerService
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime ServiceDate { get; set; }

        public Customer Customer { get; set; }
        public Service Service { get; set; }
    }

    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class OilChangeDbContext : DbContext
    {
        public OilChangeDbContext(DbContextOptions<OilChangeDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<CustomerService> CustomerServices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=oilChangeData.db");
            }
        }
    }
}