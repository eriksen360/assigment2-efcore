using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class CanteenContext : DbContext
{
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Canteen> Canteens { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Meal> Meals { get; set; }

    public string DbPath { get; }

    private const string ConnectionString = @"Data Source=(local);" +
                                             "Initial Catalog=tempdb;" +
                                             "User id=SA;" +
                                             "Password=lukmigind123!;" +
                                             // "Trusted_Connection=True;" +
                                             "TrustServerCertificate=True";


    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(ConnectionString);
}

public class Reservation
{
    public int ReservationId { get; set; }
    public string CprNumber { get; set; }
    public int CanteenId { get; }
    public int MealId { get; }
    public bool Cancelled { get; set; }

    public Canteen Canteen { get; set; }
    public Meal Meal { get; set; }
}

public class Canteen 
{
    public int CanteenId { get; set; }
    public int MenuId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public virtual Menu Menu { get; set; }
}

public class Menu
{
    public int MenuId { get; set; }
    public string MenuType { get; set; }
    public DateTime Date { get; set; }
    public int CanteenId { get; set; }

    public Canteen Canteen { get; set; }
}

public class Rating
{
    public int RatingId { get; set; }
    public int CanteenId { get; set; }
    public int RatingValue { get; set; }
    public int CprNumber { get; set; }
    public DateTime Date { get; set; }
    public Canteen Canteen { get; set; }
}

public class Customer
{
    [Key]
    public string CprNumber { get; set; } // Primary key 
    public string Name { get; set; }
}


public class Meal {
    public int MealId { get; set; }
    public int MenuId { get; set; }
    public string MealType { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }

    public Menu Menu { get; set; }
}