using Microsoft.EntityFrameworkCore;

namespace OilChangeApp.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int? CarId { get; set; }
        public Car Car { get; set; }
        public List<CustomerService> CustomerServices { get; set; }
    }

    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public List<CustomerServiceDetail> CustomerServiceDetails { get; set; }
    }

    public class CustomerService
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? OilId { get; set; }
        public DateTime ServiceDate { get; set; }
        public long Kilometers { get; set; }
        public long NextServiceKilometers { get; set; }

        public Customer Customer { get; set; }

        public Oil Oil { get; set; }
        public List<CustomerServiceDetail> CustomerServiceDetails { get; set; }
    }

    public class CustomerServiceDetail
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public Service Services { get; set; }
        public CustomerService CustomerService { get; set; }
    }

    public class Oil
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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
        public DbSet<Car> Cars { get; set; }
        public DbSet<CustomerService> CustomerServices { get; set; }
        public DbSet<Oil> Oils { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=oilChangeData.db");
            }
        }
    }
}