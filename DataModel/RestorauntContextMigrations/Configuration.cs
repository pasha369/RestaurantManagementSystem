using System.Collections.Generic;
using DataModel.Model;

namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataModel.Contexts.RestorauntDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"RestorauntContextMigrations";
        }

        protected override void Seed(DataModel.Contexts.RestorauntDbContext context)
        {
            // Country
            context.Countries.AddOrUpdate(x => x.Id,
                new Country() { Id = 1, Name = "Angola" },
                new Country() { Id = 2, Name = "Albania" },
                new Country() { Id = 3, Name = "Australia" },
                new Country() { Id = 4, Name = "Bahrain" },
                new Country() { Id = 5, Name = "Brazil" },
                new Country() { Id = 6, Name = "Denmark" },
                new Country() { Id = 7, Name = "France" },
                new Country() { Id = 8, Name = " Germany" },
                new Country() { Id = 9, Name = " Ukraine" }
                );
            // City
            context.Cities.AddOrUpdate(x => x.Id,
                new City() { Id = 1, Name = "Paris", Country = context.Countries.FirstOrDefault(c => c.Id == 7) },
                new City() { Id = 2, Name = "Bavaria", Country = context.Countries.FirstOrDefault(c => c.Id == 8) },
                new City() { Id = 3, Name = "Rivne", Country = context.Countries.FirstOrDefault(c => c.Id == 9) }
                );
            // Cuisine
            context.Cuisines.AddOrUpdate(x => x.Id,
                new Cuisine() { Id = 1, Name = "American" },
                new Cuisine() { Id = 2, Name = "Chinese" },
                new Cuisine() { Id = 3, Name = "Continental" },
                new Cuisine() { Id = 4, Name = "Cuban" }
                );
            // Restaurant
            //context.Restoraunts.AddOrUpdate(x => x.Id,
            //         new Restaurant()
            //         {
            //             Id = 1,
            //             Name = "Almeida",
            //             Description = "The route between the Almeida theatre over the road and this D&D" +
            //                           " London restaurant is a well-trodden one: visit of an evening and there’s an exodus before curtain-up." +
            //                           "cooking, which is broadly modern French with a few excursions around Europe and Britain",
            //             Cuisines = new List<Cuisine>
            //             {
            //                 context.Cuisines.FirstOrDefault(c => c.Id == 1)
            //             }
            //         },
            //         new Restaurant()
            //         {
            //             Id = 2,
            //             Name = "Antepliler",
            //             Description = "There are two very different Anteplilers in north London. " +
            //                           "The Green Lanes branch is a straightforward, functional canteen, " +
            //                           "but this time we visited the Upper Street restaurant, which feels like it’s been lifted straight out of a lifestyle magazine.",
            //             Cuisines = new List<Cuisine>
            //             {
            //                 context.Cuisines.FirstOrDefault(c => c.Id == 2)
            //             },
            //             Adress = new Address()
            //                          {
            //                              Country = context.Countries.FirstOrDefault(c => c.Id == 7),
            //                          }
            //         },
            //         new Restaurant()
            //         {
            //             Id = 3,
            //             Name = "Elk in the Woods",
            //             Description = "Having started out as a bar, the Elk presents more of a gastro " +
            //                           "face during busy lunchtimes, but regulars to this Camden Passage " +
            //                           "haunt aren’t discouraged from ordering up a Cucumber Martini (with Hendrick’s gin, £8) or Tobia Rioja and occupying a wooden table for a while.",
            //             Cuisines = new List<Cuisine>
            //             {
            //                 context.Cuisines.FirstOrDefault(c => c.Id == 2)
            //             },
            //             Adress = new Address()
            //             {
            //                 Country = context.Countries.FirstOrDefault(c => c.Id == 8),
            //             }
            //         },
            //         new Restaurant()
            //         {
            //             Id = 4,
            //             Name = "Halza",
            //             Description = "The Green Lanes branch is a straightforward, functional canteen, ",
            //             Cuisines = new List<Cuisine>
            //             {
            //                 context.Cuisines.FirstOrDefault(c => c.Id == 2)
            //             },
            //             Adress = new Address()
            //                          {
            //                              Country = context.Countries.FirstOrDefault(c => c.Id == 9),
            //                          }
            //         },
            //         new Restaurant()
            //         {
            //             Id = 5,
            //             Name = "Affer",
            //             Description = "There are two very different Anteplilers in north London. ",
            //             Cuisines = new List<Cuisine>{context.Cuisines.FirstOrDefault(c => c.Id == 2)},
            //             Adress = new Address(){Country = context.Countries.FirstOrDefault(c => c.Id == 8)}
            //         }
            //         );
            // User Info
            //context.UserInfos.AddOrUpdate(x => x.Id,
            //    new Model.UserInfo()
            //    {
            //        Id = 1,
            //        About = "Personal identity is the unique identity of persons " +
            //                "through time. That is to say, the necessary and sufficient " +
            //                "conditions under which a person at one time and " +
            //                "a person at another time can be said to be the " +
            //                "same person, persisting through time",
            //        Email = "sample@ukr.net",
            //        Facebook = "sample",
            //        Name = "Bacevich Andrew",
            //        Login = "Bac",
            //        Password = "123",
            //        Phone = "(094)1233432",
            //        PhotoUrl = "",
            //        Position = Role.Restaurateur,
            //        Skype = "Bac"
            //    },
            //    new Model.UserInfo()
            //    {
            //        Id = 2,
            //        About = "Personal identity is the unique identity of persons " +
            //                "through time. That is to say, the necessary and sufficient " +
            //                "conditions under which a person at one time and " +
            //                "a person at another time can be said to be the " +
            //                "same person, persisting through time",
            //        Email = "sample1@ukr.net",
            //        Facebook = "sample1",
            //        Name = "Becker Carl",
            //        Login = "Bec",
            //        Password = "123",
            //        Phone = "(094)1233432",
            //        PhotoUrl = "",
            //        Position = Role.Restaurateur,
            //        Skype = "Bec"
            //    },
            //    new Model.UserInfo()
            //    {
            //        Id = 3,
            //        About = "Personal identity is the unique identity of persons " +
            //                "through time. That is to say, the necessary and sufficient " +
            //                "conditions under which a person at one time and " +
            //                "a person at another time can be said to be the " +
            //                "same person, persisting through time",
            //        Email = "sample2@ukr.net",
            //        Facebook = "sample2",
            //        Name = "Bach Richard",
            //        Login = "Bah",
            //        Password = "123",
            //        Phone = "(094)1233432",
            //        PhotoUrl = "",
            //        Position = Role.User,
            //        Skype = "Bah"
            //    });
            //// Client
            //context.ClientInfos.AddOrUpdate(x => x.Id,
            //    new ClientInfo()
            //    {
            //        Id = 1,
            //        Restaurant = context.Restoraunts.FirstOrDefault(r => r.Id == 1),
            //        UserInfo = context.UserInfos.FirstOrDefault(u => u.Id == 1)
            //    },
            //    new ClientInfo()
            //    {
            //        Id = 2,
            //        Restaurant = context.Restoraunts.FirstOrDefault(r => r.Id == 2),
            //        UserInfo = context.UserInfos.FirstOrDefault(u => u.Id == 2)
            //    });

            context.SaveChanges();

        }
    }
}
