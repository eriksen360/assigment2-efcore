using EF_Core.Models;
using Microsoft.EntityFrameworkCore;


namespace EF_Core.Data
{
    public class CanteenContext : DbContext
    {
        private const string ConnectionString = $"Data Source=localhost;" +
                                                $"Initial Catalog=Assignment_2;" +
                                                $"User ID=SA;" +
                                                $"Password=Twb28xxm123;" +
                                                $"Connect Timeout=30;" +
                                                $"Encrypt=False;" +
                                                $"Trust Server Certificate=False;" +
                                                $"Application Intent=ReadWrite;" +
                                                $"Multi Subnet Failover=False";

        public DbSet<Canteen> Canteens { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Meal> Meals { get; set; } 
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(ConnectionString);
    }
}
