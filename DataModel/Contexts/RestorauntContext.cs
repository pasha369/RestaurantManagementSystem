using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Model;

namespace DataModel.Contexts
{
    public class RestorauntDbContext : DbContext
    {
        //public static RestorauntDbContext context = new RestorauntDbContext();

        public RestorauntDbContext()
            : base("RestorauntDbEntities")
        {
            Database.SetInitializer<RestorauntDbContext>(null);
        }
        /// <summary>
        /// Resolve problem with cascade delete
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // remove any children first
            //modelBuilder.Entity<Restaurant>()
            //    .HasOptional(a => a.Halls)
            //    .WithOptionalDependent()
            //    .WillCascadeOnDelete(true);
            //modelBuilder.Entity<Hall>()
            //    .HasOptional(a => a.Tables)
            //    .WithOptionalDependent()
            //    .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cuisine> Cuisines { set; get; }
        public DbSet<Restaurant> Restoraunts { set; get; }
        public DbSet<Hall> Halls { set; get; }
        public DbSet<DinnerTable> Tables { set; get; }

        public DbSet<Review> Reviews { set; get; }

        public DbSet<UserInfo> UserInfos { set; get; }
        public DbSet<CustomerInfo> CustomerInfos { set; get; }

        public DbSet<ClientInfo> ClientInfos { set; get; }

        public DbSet<Menu> Menu { set; get; }
        public DbSet<Dish> Dishes { set; get; }
        public DbSet<Category> Categories { set; get; }

        public DbSet<Receipt> Orders { set; get; }

        public DbSet<Country> Countries { set; get; }
        public DbSet<City> Cities { set; get; }

        public DbSet<Reservation> Reservations { set; get; }
        public DbSet<Favorite> Favorites { set; get; }
    }
}
