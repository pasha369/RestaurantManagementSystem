using System.Data.Entity;
using DataModel.Model;

namespace DataModel.Contexts
{
    public class RestorauntDbContext : DbContext
    {
        public RestorauntDbContext()
            : base("name=RestorauntDbEntities")
        {
            Database.SetInitializer<RestorauntDbContext>(null);
        }

        public virtual DbSet<Cuisine> Cuisines { set; get; }
        public virtual DbSet<Restaurant> Restoraunts { set; get; }
        public virtual DbSet<Hall> Halls { set; get; }
        public virtual DbSet<DinnerTable> Tables { set; get; }
        public virtual DbSet<Review> Reviews { set; get; }
        public virtual DbSet<UserInfo> UserInfos { set; get; }
        public virtual DbSet<CustomerInfo> CustomerInfos { set; get; }
        public virtual DbSet<ClientInfo> ClientInfos { set; get; }
        public virtual DbSet<Menu> Menu { set; get; }
        public virtual DbSet<Dish> Dishes { set; get; }
        public virtual DbSet<Category> Categories { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<Country> Countries { set; get; }
        public virtual DbSet<City> Cities { set; get; }
        public virtual DbSet<Reservation> Reservations { set; get; }
        public virtual DbSet<Favorite> Favorites { set; get; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
    }
}
