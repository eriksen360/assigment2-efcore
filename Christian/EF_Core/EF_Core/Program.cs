using EF_Core.Data;
using EF_Core.Models;

using (var db = new CanteenContext())
{
    //Seed(db);

    // Query 1 - Getting the daily Menu options for Kgl. Bibliotek
    Console.WriteLine("Query 1\n--------------------");
    var kgl_id = db.Canteens.Where(c => c.Name == "Kgl. Bibliotek").First().CanteenId;
    var kgl_daily_id = db.Menus.Where(m => m.CanteenId == kgl_id).First().MenuId;
    var kgl_daily = db.Meals.Where(m => m.MenuId == kgl_daily_id).ToList();
    kgl_daily.ForEach(m =>  Console.WriteLine(m.MealType.ToString() + ": " + m.Name.ToString()));

    // Query 2 - Getting the reservations for Customer with CPR 111111-1111
    Console.WriteLine("\nQuery 2\n--------------------");
    var reservations = from r in db.Reservations
                       from c in db.Canteens
                       from m in db.Meals
                       where r.CanteenId == c.CanteenId && r.MealId == m.MealId && r.CprNumber == "111111-1111"
                       select new { MealId = r.MealId, MealName = m.Name, CanteenName = c.Name };

    foreach (var item in reservations)
        Console.WriteLine("Meal Id: " + item.MealId + "\nName: " + item.MealName + "\nCanteen Name: " + item.CanteenName + "\n");

    // Query 3 - Getting the number of reservations for each of the daily menu options for Kgl. Bibliotek
    Console.WriteLine("Query 3\n--------------------");
    var kgl_warm_id = db.Meals.Where(m => m.MenuId == kgl_daily_id && m.MealType == "Warm").First().MealId;
    var kgl_street_id = db.Meals.Where(m => m.MenuId == kgl_daily_id && m.MealType == "Street").First().MealId;

    /*var reservations_count = from r in db.Reservations
                             where (r.MealId == kgl_warm_id || r.MealId == kgl_street_id) && r.CanteenId == kgl_id
                             group r by r.MealId into mealIdGroup
                             select new { Name = mealIdGroup.Key, Count = mealIdGroup.Count() };

    foreach (var item in reservations_count)
        Console.WriteLine("Meal Id: " + item.Name + " Amount: " + item.Count);*/

    var warm_count = (from r in db.Reservations
                where r.MealId == kgl_warm_id
                select r.MealId).Count();
    var warm_name = db.Meals.Where(m => m.MealId == kgl_warm_id).Single().Name;
    Console.WriteLine("Name: " + warm_name + " - Amount: " + warm_count);

    var street_count = (from r in db.Reservations
                      where r.MealId == kgl_street_id
                      select r.MealId).Count();
    var street_name = db.Meals.Where(m => m.MealId == kgl_street_id).Single().Name;
    Console.WriteLine("Name: " + street_name + " - Amount: " + street_count);

    // Query 4 - Getting the Just-In-Time and Cancelled meal options for Kgl. Bibliotek
    Console.WriteLine("\nQuery 4\n--------------------");
    var kgl_jit_id = db.Menus.Where(m => m.CanteenId == kgl_id && m.MenuType == "Just-In-Time").First().MenuId;
    var kgl_canc_id = db.Menus.Where(m => m.CanteenId == kgl_id && m.MenuType == "Cancelled").First().MenuId;

    var meals = from m in db.Meals
                where m.MenuId == kgl_jit_id || m.MenuId == kgl_canc_id
                select m.Name;

    foreach (var item in meals)
        Console.WriteLine(item);

    // Query 5 - Getting the cancelled daily menu meals from the nearby canteens
    Console.WriteLine("\nQuery 5\n--------------------");
    var nearby_meals = from m in db.Menus
                       from c in db.Canteens
                       from meal in db.Meals
                       where m.MenuType == "Cancelled" && c.CanteenId != kgl_id && c.Location == "Aarhus" && m.CanteenId == c.CanteenId && m.MenuId == meal.MenuId
                       select new { CanteenName = c.Name, MealName = meal.Name };

    foreach (var item in nearby_meals)
        Console.WriteLine(item.CanteenName + ": " + item.MealName);

    // Query 6 - Getting the average ratings from all the canteens
    Console.WriteLine("\nQuery 6\n--------------------");
    var kem_id = db.Canteens.Where(c => c.Name == "Kemisk Canteen").First().CanteenId;
    var mat_id = db.Canteens.Where(c => c.Name == "Matematisk Canteen").First().CanteenId;

    var kgl_avg = (from r in db.Ratings
                  where r.CanteenId == kgl_id
                  select r.RatingValue).Average();
    var kgl_name = db.Canteens.Where(c => c.CanteenId == kgl_id).Single().Name;
    Console.WriteLine("Name: " + kgl_name + " - Rating: " + kgl_avg);

    var kem_avg = (from r in db.Ratings
                   where r.CanteenId == kem_id
                   select r.RatingValue).Average();
    var kem_name = db.Canteens.Where(c => c.CanteenId == kem_id).Single().Name;
    Console.WriteLine("Name: " + kem_name + " - Rating: " + kem_avg);

    var mat_avg = (from r in db.Ratings
                   where r.CanteenId == mat_id
                   select r.RatingValue).Average();
    var mat_name = db.Canteens.Where(c => c.CanteenId == mat_id).Single().Name;
    Console.WriteLine("Name: " + mat_name + " - Rating: " + mat_avg);


    //ClearData(db);
}

void Seed(CanteenContext context)
{
    // Adding Canteens
    context.Canteens.Add(new Canteen { Name = "Kgl. Bibliotek", Location = "Aarhus" });
    context.Canteens.Add(new Canteen { Name = "Kemisk Canteen", Location = "Aarhus" });
    context.Canteens.Add(new Canteen { Name = "Matematisk Canteen", Location = "Aarhus" });
    context.SaveChanges();

    // Adding Customers
    context.Customers.Add(new Customer { CprNumber = "111111-1111", Name = "Christian" });
    context.Customers.Add(new Customer { CprNumber = "222222-2222", Name = "Robin" });
    context.Customers.Add(new Customer { CprNumber = "333333-3333", Name = "Mathias" });
    context.Customers.Add(new Customer { CprNumber = "444444-4444", Name = "Aleksander" });
    context.SaveChanges();

    //Adding Menus
    var kgl_id = context.Canteens.Where(c => c.Name == "Kgl. Bibliotek").First().CanteenId;
    var kemisk_id = context.Canteens.Where(c => c.Name == "Kemisk Canteen").First().CanteenId;
    var matematisk_id = context.Canteens.Where(c => c.Name == "Matematisk Canteen").First().CanteenId;

    context.Menus.Add(new Menu { CanteenId = kgl_id, MenuType = "Daily Menu", Date = DateTime.Now });
    context.Menus.Add(new Menu { CanteenId = kgl_id, MenuType = "Just-In-Time", Date = DateTime.Now });
    context.Menus.Add(new Menu { CanteenId = kgl_id, MenuType = "Cancelled", Date = DateTime.Now });
    context.Menus.Add(new Menu { CanteenId = kemisk_id, MenuType = "Daily Menu", Date = DateTime.Now });
    context.Menus.Add(new Menu { CanteenId = kemisk_id, MenuType = "Just-In-Time", Date = DateTime.Now });
    context.Menus.Add(new Menu { CanteenId = kemisk_id, MenuType = "Cancelled", Date = DateTime.Now });
    context.Menus.Add(new Menu { CanteenId = matematisk_id, MenuType = "Daily Menu", Date = DateTime.Now });
    context.Menus.Add(new Menu { CanteenId = matematisk_id, MenuType = "Just-In-Time", Date = DateTime.Now });
    context.Menus.Add(new Menu { CanteenId = matematisk_id, MenuType = "Cancelled", Date = DateTime.Now });
    context.SaveChanges();

    // Adding Ratings
    var chr_cpr = context.Customers.Where(c => c.CprNumber == "111111-1111").First().CprNumber;
    var rob_cpr = context.Customers.Where(c => c.CprNumber == "222222-2222").First().CprNumber;
    var mat_cpr = context.Customers.Where(c => c.CprNumber == "333333-3333").First().CprNumber;
    var alek_cpr = context.Customers.Where(c => c.CprNumber == "444444-4444").First().CprNumber;

    context.Ratings.Add(new Rating { CanteenId = kgl_id, CprNumber = chr_cpr, RatingValue = 4, Date = DateTime.Now });
    context.Ratings.Add(new Rating { CanteenId = kgl_id, CprNumber = rob_cpr, RatingValue = 3, Date = DateTime.Now });
    context.Ratings.Add(new Rating { CanteenId = kgl_id, CprNumber = mat_cpr, RatingValue = 1, Date = DateTime.Now });
    context.Ratings.Add(new Rating { CanteenId = kgl_id, CprNumber = alek_cpr, RatingValue = 5, Date = DateTime.Now });
    context.Ratings.Add(new Rating { CanteenId = kemisk_id, CprNumber = chr_cpr, RatingValue = 2, Date = DateTime.Now });
    context.Ratings.Add(new Rating { CanteenId = kemisk_id, CprNumber = rob_cpr, RatingValue = 5, Date = DateTime.Now });
    context.Ratings.Add(new Rating { CanteenId = kemisk_id, CprNumber = mat_cpr, RatingValue = 4, Date = DateTime.Now });
    context.Ratings.Add(new Rating { CanteenId = kemisk_id, CprNumber = alek_cpr, RatingValue = 3, Date = DateTime.Now });
    context.Ratings.Add(new Rating { CanteenId = matematisk_id, CprNumber = chr_cpr, RatingValue = 5, Date = DateTime.Now });
    context.Ratings.Add(new Rating { CanteenId = matematisk_id, CprNumber = rob_cpr, RatingValue = 5, Date = DateTime.Now });
    context.Ratings.Add(new Rating { CanteenId = matematisk_id, CprNumber = mat_cpr, RatingValue = 4, Date = DateTime.Now });
    context.Ratings.Add(new Rating { CanteenId = matematisk_id, CprNumber = alek_cpr, RatingValue = 4, Date = DateTime.Now });
    context.SaveChanges();

    // Adding Meals
    var kgl_daily_id = context.Menus.Where(m => m.CanteenId == kgl_id && m.MenuType == "Daily Menu").First().MenuId;
    var kgl_jit_id = context.Menus.Where(m => m.CanteenId == kgl_id && m.MenuType == "Just-In-Time").First().MenuId;
    var kgl_canc_id = context.Menus.Where(m => m.CanteenId == kgl_id && m.MenuType == "Cancelled").First().MenuId;
    var kem_daily_id = context.Menus.Where(m => m.CanteenId == kemisk_id && m.MenuType == "Daily Menu").First().MenuId;
    var kem_jit_id = context.Menus.Where(m => m.CanteenId == kemisk_id && m.MenuType == "Just-In-Time").First().MenuId;
    var kem_canc_id = context.Menus.Where(m => m.CanteenId == kemisk_id && m.MenuType == "Cancelled").First().MenuId;
    var mat_daily_id = context.Menus.Where(m => m.CanteenId == matematisk_id && m.MenuType == "Daily Menu").First().MenuId;
    var mat_jit_id = context.Menus.Where(m => m.CanteenId == matematisk_id && m.MenuType == "Just-In-Time").First().MenuId;
    var mat_canc_id = context.Menus.Where(m => m.CanteenId == matematisk_id && m.MenuType == "Cancelled").First().MenuId;

    context.Meals.Add(new Meal { MenuId = kgl_daily_id, Name = "Green Curry", MealType = "Warm", Quantity = null });
    context.Meals.Add(new Meal { MenuId = kgl_daily_id, Name = "Pizza", MealType = "Street", Quantity = null });
    context.Meals.Add(new Meal { MenuId = kgl_jit_id, Name = "Sandwich", MealType = "JIT", Quantity = 25 });
    context.Meals.Add(new Meal { MenuId = kgl_jit_id, Name = "Soup", MealType = "JIT", Quantity = 30 });
    context.Meals.Add(new Meal { MenuId = kgl_canc_id, Name = "Pizza", MealType = "Cancelled", Quantity = 3 });

    context.Meals.Add(new Meal { MenuId = kem_daily_id, Name = "Lasagne", MealType = "Warm", Quantity = null });
    context.Meals.Add(new Meal { MenuId = kem_daily_id, Name = "Burger", MealType = "Street", Quantity = null });
    context.Meals.Add(new Meal { MenuId = kem_jit_id, Name = "Sandwich", MealType = "JIT", Quantity = 40 });
    context.Meals.Add(new Meal { MenuId = kem_jit_id, Name = "Fries", MealType = "JIT", Quantity = 50 });
    context.Meals.Add(new Meal { MenuId = kem_canc_id, Name = "Lasagne", MealType = "Cancelled", Quantity = 5 });

    context.Meals.Add(new Meal { MenuId = mat_daily_id, Name = "Steak", MealType = "Warm", Quantity = null });
    context.Meals.Add(new Meal { MenuId = mat_daily_id, Name = "Pita", MealType = "Street", Quantity = null });
    context.Meals.Add(new Meal { MenuId = mat_jit_id, Name = "Sandwich", MealType = "JIT", Quantity = 15 });
    context.Meals.Add(new Meal { MenuId = mat_jit_id, Name = "Cake", MealType = "JIT", Quantity = 35 });
    context.Meals.Add(new Meal { MenuId = mat_canc_id, Name = "Pita", MealType = "Cancelled", Quantity = 2 });
    context.SaveChanges();

    // Adding Reservations (Only for Kgl. Bibliotek)
    var kgl_warm_id = context.Meals.Where(m => m.MenuId == kgl_daily_id && m.MealType == "Warm").First().MealId;
    var kgl_street_id = context.Meals.Where(m => m.MenuId == kgl_daily_id && m.MealType == "Street").First().MealId;
    var kem_warm_id = context.Meals.Where(m => m.MenuId == kem_daily_id && m.MealType == "Warm").First().MealId;
    var kem_street_id = context.Meals.Where(m => m.MenuId == kem_daily_id && m.MealType == "Street").First().MealId;
    var mat_warm_id = context.Meals.Where(m => m.MenuId == mat_daily_id && m.MealType == "Warm").First().MealId;
    var mat_street_id = context.Meals.Where(m => m.MenuId == mat_daily_id && m.MealType == "Street").First().MealId;

    context.Reservations.Add(new Reservation { CanteenId = kgl_id, MealId = kgl_warm_id, CprNumber = chr_cpr, Cancelled = false });
    context.Reservations.Add(new Reservation { CanteenId = kgl_id, MealId = kgl_warm_id, CprNumber = rob_cpr, Cancelled = false });
    context.Reservations.Add(new Reservation { CanteenId = kgl_id, MealId = kgl_warm_id, CprNumber = mat_cpr, Cancelled = false });
    context.Reservations.Add(new Reservation { CanteenId = kgl_id, MealId = kgl_street_id, CprNumber = alek_cpr, Cancelled = false });
    context.SaveChanges();
}
void ClearData(CanteenContext context)
{
    var canteens = context.Canteens.ToList();
    var customers = context.Customers.ToList();
    var ratings = context.Ratings.ToList();
    var menus = context.Menus.ToList();
    var meals = context.Meals.ToList();
    var reservations = context.Reservations.ToList();

    context.RemoveRange(reservations);
    context.RemoveRange(meals);
    context.RemoveRange(menus);
    context.RemoveRange(ratings);
    context.RemoveRange(customers);
    context.RemoveRange(canteens);

    context.SaveChanges();
}