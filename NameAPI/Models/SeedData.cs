using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NameBandit.Data;
using NameBandit.Helpers;

namespace NameBandit {

    public class SeedData {


        public static void SeedDatabase(ApplicationDbContext context) {
            if (context.Database.GetMigrations().Count() > 0
                    && context.Database.GetPendingMigrations().Count() == 0
                    && context.Names.Count() == 0
                    && context.Categories.Count() == 0) {

                var names = new List<Name>();

                names = ScrapingHelper.ScapeNames(names, "http://www.urd.dk/fornavne/drenge.htm"); 
                names = ScrapingHelper.ScapeNames(names, "http://www.urd.dk/fornavne/piger.htm", true);

                foreach (var name in names) {
                    name.Vibration = CalculationHelper.CalculateNameVibration(name.Text);
                }

                context.Names.AddRange(names.Distinct().ToList());

                context.SaveChanges();                

                #region Categories

                if (context.Categories.Count() == 0) {
                    var bibleList = new List<string>() { "Adam", "Alexander", "Aleksander", "Benjamin", "Rakel", "Rachel", "Cornelius", "David", "Goliat", "Goliath", "Elias", "Gabriel", "Immanuel", "Emmanuel", "Isak", "Sara", "Sarah", "Jakob", "Jacob", "Johannes", "John", "Hans", "Jesus", "Jonatan", "Jonathan", "Lukas", "Lucas", "Markus", "Marcus", "Mattias", "Mathias", "Matias", "Matthæus", "Mikael", "Michael", "Noah", "Noa", "Silas", "Thomas", "Ada", "Elisabeth", "Elizabeth", "Elisabet", "Elizabet", "Ester", "Esther", "Eva", "Johanna", "Joanna", "Lea", "Leah", "Maria", "Mariah", "Rebekka", "Rebecca" };

                    var Bible = new Category() {
                        Title = "Bible",
                        Names = new List<Name>()
                    };

                    foreach (var name in bibleList) {
                        var obj = context.Names.Where(x => x.Text == name).FirstOrDefault();
                        if (obj != null)  {
                            Bible.Names.Add(obj);
                        }                    
                    }

                    var starWarsList = new List<string>() { "Anakin", "Luke", "Leia", "Jar-Jar", "Qui-Gon", "Obi-Wan", "Han", "Solo", "Darth", "Lando", "Yoda" };

                    var StarWars = new Category() {
                        Title = "Star Wars",
                        Names = new List<Name>()
                    };

                    foreach (var name in starWarsList) {
                        var obj = context.Names.Where(x => x.Text == name).FirstOrDefault();
                        if (obj != null)  {
                            StarWars.Names.Add(obj);
                        }                    
                    }

                    var starTrekList = new List<string>() { "Jean-Luc", "Kirk" };
                    
                    var tolkienList = new List<string>() { "Frodo", "Bilbo", "Gandalf", "Sauron" };
                    
                    var harryPotterList = new List<string>() { "Harry", "Ron", "Ronald", "Hermione", "Hagrid", "Albus", "Vernon", "Petunia", "Dudley", "Ginny", "Fred", "George", "Lavender", "Fluffy", "Tom" };
                    
                    var vikingsList = new List<string>() { "Sven" };
                    
                    var famousPeople = new List<string>() { "Elvis", "Clapton", "Armstrong", "Figo", "Pele", "Ronaldo", "Kaka", "Zlatan" };
                    
                    var instrumentsList = new List<string>() { "Cello" };

                    var willyList = new List<string>() { "Willy", "Peter", "Pete", "Diller", "Pik", "Penis" };

                    var animalsList = new List<string>() { "Bjørn", "Ulv", "Cobra", "Kobra", "Haj", "Havand", "Musling", "Panda", "Kamel" };
                    
                    var plantsList = new List<string>() { "Hyben", "Mynte" };

                    var naturesList = new List<string>() { "Sne", "Sommer", "Sol", "Sea" };
                    
                    var coloursList = new List<string>() { "Gul" };

                    var emotionsList = new List<string>() { "Håb", "Triumf", "Sejr" };

                    var liquorList = new List<string>() { "Gin" };

                    var titleList = new List<string>() { "Skipper", "Kong", "King", "Prince", "Queen", "Prince", "Princess", "Lord", "Sir", "Brormand", "Skat", "Fru", "Viking" };

                    var funnyCombinations =  new List<string>() { "Lucky Luke", "Anders Sand", "Teddy Bjørn", "Dan Mark", "Mette Vuns" };

                    var justFunnyList = new List<string>() { "Ninja", "Awesome", "Blær", "Dreng", "Snoopy", "Charme", "Nitte", "Sok", "Piphat", "Pop", "Fe", "Gift", "Engel", "Offer" };

                    context.Categories.AddRange(
                        Bible,
                        StarWars
                    );
                }
                #endregion

                context.SaveChanges();
            }
        }
    }
}
