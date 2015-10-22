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
            context.Cuisines.AddOrUpdate(x => x.Id,
                new Cuisine() {Id = 1, Name = "American" },
                new Cuisine() {Id = 2, Name = "Chinese" },
                new Cuisine() {Id = 3, Name = "Continental" },
                new Cuisine() {Id = 4, Name = "Cuban" }
                );

            context.Restoraunts.AddOrUpdate(x => x.Id,
                     new Restaurant()
                     {
                         Id = 1,
                         Name = "Almeida",
                         Description = "The route between the Almeida theatre over the road and this D&D London restaurant is a well-trodden one: visit of an evening and there’s an exodus before curtain-up. The pre-theatre menu here, then, is often just that – and is excellent value at £17 for two courses, £20 for three. Those without a show to rush to can take more time over the sophisticated cooking, which is broadly modern French with a few excursions around Europe and Britain." +
                                       "On our last visit, Cornish pollock was paired with golden sultana and cauliflower couscous, and a risotto was rich with wild mushrooms and parmesan; both were from the set menu and served beautifully in glazed pottery dishes. Such thoughtful touches set the standard high – crisp water biscuits with the cheese were seeded and clearly own-made; the charcuterie board is a rustic plank of rillettes, terrines and scotch eggs, all produced in-house." +
                                       "The open kitchen looks on to a discreetly elegant, modern room enlivened by a vast, colourful mural and broadsided by a small bar with its own food menu. As a special-occasion alternative to the many restaurants in Islington, Almeida is a star.",
                         Cuisines = new List<Cuisine>
                         {
                             context.Cuisines.FirstOrDefault(c => c.Id == 1)
                         }
                     },
                     new Restaurant()
                     {
                         Id = 2,
                         Name = "Antepliler",
                         Description = "There are two very different Anteplilers in north London. The Green Lanes branch is a straightforward, functional canteen, but this time we visited the Upper Street restaurant, which feels like it’s been lifted straight out of a lifestyle magazine. Blue neon mosaics on a black background, making the venue resemble an Ottoman-themed nightclub, are an acquired taste. Thankfully there’s nothing showy about the food, some of which originates in Gaziantep province, near the border with Syria." +
                                       " One south-eastern dish we haven’t seen elsewhere in London was a highlight: cig köfte combines raw lamb with bulgar wheat, chilli, garlic and parsley to fantastic effect. The vegetarian version of the dish made with lentils was almost as good, and we scooped up both with wonderfully light, puffy flatbread. We also loved Antepliler’s houmous: thick, creamy and enhanced by a sprinkling of sumac. Ali nazak, a main course of diced lamb with yoghurt and mashed smoked aubergine, had been correctly prepared," +
                                       " but we found the combination overwhelmingly rich. Lamb adana kebab, another recipe from Turkey’s south-east, was simple, well-proportioned and skilfully executed. The wine list is short and fairly predictable, but service is charming and the pre-theatre menu is an excellent deal. Impressive.",
                         Cuisines = new List<Cuisine>
                         {
                             context.Cuisines.FirstOrDefault(c => c.Id == 1)
                         }
                     },
                     new Restaurant()
                     {
                         Id = 3,
                         Name = "Elk in the Woods",
                         Description = "Having started out as a bar, the Elk presents more of a gastro face during busy lunchtimes, but regulars to this Camden Passage haunt aren’t discouraged from ordering up a Cucumber Martini (with Hendrick’s gin, £8) or Tobia Rioja and occupying a wooden table for a while. Martinis, seven in number (Polish with Zubrówka Bison Grass vodka, Watermelon & Basil with Pinky vodka) dominate the cocktail list, but you’ll also find zingy options such as a Grapefruit Mojito (with Matusalem Classico and fresh mint) or the Elderflower Fizz (with Buffalo Trace bourbon). Draught beers include Sagres, Kronenbourg and Theakston’s ale, with bottled Moretti, Corona and Asahi alongside, and those pregnant or driving have a few non-alcoholic cocktails to choose from: the Orange & Ginger Zing (orange juice, freshly squeezed lemon, ginger ale) hits the spot. Elk in the Woods is also a place for a quality " +
                                       "breakfast, with the likes of duck egg with asparagus, sausage and toast dippers, and over-the-top sandwiches: veal doorstep, or rare breed lamb burger in torteno roll with grilled courgette and mint jelly. ",
                         Cuisines = new List<Cuisine>
                         {
                             context.Cuisines.FirstOrDefault(c => c.Id == 2)
                         }
                     });
            context.SaveChanges();

        }
    }
}
