using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NameBandit.Data;
using NameBandit.Helpers;
using NameBandit.Models;

namespace NameBandit.Data {

    public class SeedData {


        public static void SeedDatabase(ApplicationDbContext context) {
            if (context.Database.GetMigrations().Count() > 0
                    && context.Database.GetPendingMigrations().Count() == 0
                    && context.Names.Count() == 0
                    && context.Categories.Count() == 0) {

                var names = new List<Name>();

                names = ScrapingHelper.ScapeNames(names, "http://www.urd.dk/fornavne/drenge.htm"); 
                names = ScrapingHelper.ScapeNames(names, "http://www.urd.dk/fornavne/piger.htm", true);

                names = ScrapingHelper.ScapeNames2(names);

                foreach (var name in names) {
                    name.Vibration = CalculationHelper.CalculateNameVibration(name.Text);
                }

                context.Names.AddRange(names.Distinct().ToList());

                context.SaveChanges();                

                #region Categories

                if (context.Categories.Count() == 0) {
                    var bibleList = new List<string>() { "Adam", "Alexander", "Aleksander", "Benjamin", "Rakel", "Rachel", "Cornelius", "David", "Goliat", "Goliath", "Elias", "Gabriel", "Immanuel", "Emmanuel", "Isak", "Sara", "Sarah", "Jakob", "Jacob", "Johannes", "John", "Hans", "Jesus", "Jonatan", "Jonathan", "Lukas", "Lucas", "Markus", "Marcus", "Mattias", "Mathias", "Matias", "Matthæus", "Mikael", "Michael", "Noah", "Noa", "Silas", "Thomas", "Ada", "Elisabeth", "Elizabeth", "Elisabet", "Elizabet", "Ester", "Esther", "Eva", "Johanna", "Joanna", "Lea", "Leah", "Maria", "Mariah", "Rebekka", "Rebecca" };

                    var bible = new Category() {
                        Title = "Bible",
                        Names = new List<Name>()
                    };

                    foreach (var name in bibleList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             bible.Names.Add(obj);
                        }                 
                    }

                    var starWarsList = new List<string>() { "Anakin", "Luke", "Leia", "Jar-Jar", "Qui-Gon", "Obi-Wan", "Han", "Solo", "Darth", "Lando", "Yoda" };

                    var starWars = new Category() {
                        Title = "Star Wars",
                        Names = new List<Name>()
                    };

                    foreach (var name in starWarsList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             starWars.Names.Add(obj);
                        }                   
                    }

                    var starTrekList = new List<string>() { "Jean-Luc", "Kirk" };

                    var starTrek = new Category() {
                        Title = "Star Trek",
                        Names = new List<Name>()
                    };

                    foreach (var name in starTrekList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             starTrek.Names.Add(obj);
                        }                   
                    }
                    
                    var tolkienList = new List<string>() { "Aragorn", "Arwen", "Baggins", "Balin", "Bard", "Beorn", "Beren", "Bilbo", "Bombadil", "Boromir", "Brandybuck", "Celeborn", "Déagol", "Denethor", "Dís", "Elendil", "Elrond", "Elu", "Elwing", "Éomer", "Éowyn", "Eärendil", "Faramir", "Fëanor", "Felagund", "Fingolfin", "Finrod", "Finwë", "Frodo", "Galadriel", "Gamgee", "Gandalf", "Gil-galad", "Gimli", "Glorfindel", "Goldberry", "Gollum", "Gríma ", "Húrin", "Idril", "Indis", "Isildur", "Kíli", "Legolass", "Lúthien", "Maedhros", "Melian", "Melkor", "Meriadoc", "Merry", "Míriel", "Morgoth", "Okensheild", "Peregrin", "Pippin", "Radagast", "Sam", "Samwise", "Saruman", "Sauron ", "Shelob", "Smaug", "Sméagol", "Théoden", "Thingol", "Thorin", "Thranduil", "Tom", "Took", "Treebeard", "Tuor", "Turambar", "Túrin", "Ungoliant", "Wormtongue" };

                    var tolkien = new Category() {
                        Title = "Tolkien",
                        Names = new List<Name>()
                    };

                    foreach (var name in tolkienList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             tolkien.Names.Add(obj);
                        }                   
                    }
                    
                    var harryPotterList = new List<string>() { "Harry", "Ron", "Ronald", "Hermione", "Hagrid", "Albus", "Vernon", "Petunia", "Dudley", "Ginny", "Fred", "George", "Lavender", "Fluffy", "Tom" };

                    var harryPotter = new Category() {
                        Title = "Harry Potter",
                        Names = new List<Name>()
                    };

                    foreach (var name in harryPotterList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             harryPotter.Names.Add(obj);
                        }                   
                    }

                    var greekMyhologyList = new List<string>() { "Zeus", "Muse", "Herkules", "Hercules", "Ares", "Afrodite", "Achilles", "Achilleus", "Hektor", "Hector", "Hera", "Herakles", "Atena", "Atene", "Athena", "Athene" };

                    var greekMyth = new Category() {
                        Title = "Græsk Mytologi",
                        Names = new List<Name>()
                    };

                    foreach (var name in greekMyhologyList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             greekMyth.Names.Add(obj);
                        }                   
                    }
                    
                    var norseMyhologyList = new List<string>() { "Thor", "Odin", "Loke", "Heimdal", "Frej", "Freja", "Frey", "Freya" };

                        var norseMyth = new Category() {
                        Title = "Nordisk Mytologi",
                        Names = new List<Name>()
                    };

                    foreach (var name in norseMyhologyList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             norseMyth.Names.Add(obj);
                        }                   
                    }

                    var arabianNightsList = new List<string>() { "Aladdin", "Gulnare" };

                        var arabianNights = new Category() {
                        Title = "1001 Nats Eventyr",
                        Names = new List<Name>()
                    };

                    foreach (var name in arabianNightsList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             arabianNights.Names.Add(obj);
                        }                   
                    }
                    
                    var vikingsList = new List<string>() { "Sven", "Palnatoke", "Toke" };

                        var vikings = new Category() {
                        Title = "Vikingenavne",
                        Names = new List<Name>()
                    };

                    foreach (var name in vikingsList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             vikings.Names.Add(obj);
                        }                   
                    }
                    
                    var famousPeopleList = new List<string>() { "Elvis", "Clapton", "Armstrong", "Figo", "Pele", "Ronaldo", "Kaka", "Zlatan", "Napoleon" };

                        var famous = new Category() {
                        Title = "Berømte Personer",
                        Names = new List<Name>()
                    };

                    foreach (var name in famousPeopleList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             famous.Names.Add(obj);
                        }                   
                    }

                    var fictivePeopleList = new List<string>() { "Barnaby", "Obelix", "Sherlock", "Snoopy" };

                        var fictive = new Category() {
                        Title = "Fiktive Personer",
                        Names = new List<Name>()
                    };

                    foreach (var name in fictivePeopleList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             fictive.Names.Add(obj);
                        }                   
                    }
                    
                    var instrumentsList = new List<string>() { "Cello", "Gitar", "Bas" };

                        var instruments = new Category() {
                        Title = "Musikinstrumenter",
                        Names = new List<Name>()
                    };

                    foreach (var name in instrumentsList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             instruments.Names.Add(obj);
                        }                   
                    }

                    var willyList = new List<string>() { "Willy", "Peter", "Pete", "Diller", "Pik", "Penis", "Wiener" };

                        var willies = new Category() {
                        Title = "Falloscentriske Navne",
                        Names = new List<Name>()
                    };

                    foreach (var name in willyList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             willies.Names.Add(obj);
                        }                   
                    }

                    var citiesList = new List<string>() { "Frejlev", "London", "Berlin", "York", "Nice", "Tokyo", "Madrid", "Valencia", "Sevilla", "Roma", "Milan", "Verona", "Lyon" };

                        var cities = new Category() {
                        Title = "Byer",
                        Names = new List<Name>()
                    };

                    foreach (var name in citiesList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             cities.Names.Add(obj);
                        }                   
                    }

                    var animalsList = new List<string>() { "Bjørn", "Ulv", "Cobra", "Kobra", "Haj", "Havand", "Musling", "Panda", "Kamel", "Løve", "Tiger", "Panthera", "Lion", "Svane", "Spurv" };

                        var animals = new Category() {
                        Title = "Dyreriget",
                        Names = new List<Name>()
                    };

                    foreach (var name in animalsList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             animals.Names.Add(obj);
                        }                   
                    }
                    
                    var plantsList = new List<string>() { "Hyben", "Mynte", "Solsikke", "Birk", "Amaryllis", "Hyachint", "Eg", "Viol", "Rose", "Oregano", "Timian", "Persille", "Lavendel", "Kløver", "Rosmarin" };

                        var plants = new Category() {
                        Title = "Planteriget",
                        Names = new List<Name>()
                    };

                    foreach (var name in plantsList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             plants.Names.Add(obj);
                        }                   
                    }

                    var naturesList = new List<string>() { "Sne", "Sommer", "Sol", "Sea", "Sten" };

                        var nature = new Category() {
                        Title = "Naturen",
                        Names = new List<Name>()
                    };

                    foreach (var name in naturesList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             nature.Names.Add(obj);
                        }                   
                    }
                    
                    var coloursList = new List<string>() { "Gul", "Violet", "Azur", "Blå", "Brun", "Cyan", "Grå", "Grøn", "Hvid", "Indigo", "Lilla", "Lyserød", "Pink", "Okker", "Purpur", "Rosa", "Rød", "Sort", "Umbra", "Lime", "Lyseblå", "Lysegrøn", "Orange" };

                        var colours = new Category() {
                        Title = "Farver",
                        Names = new List<Name>()
                    };

                    foreach (var name in coloursList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             colours.Names.Add(obj);
                        }                   
                    }

                    var emotionsList = new List<string>() { "Håb", "Triumf", "Sejr" };

                        var emotions = new Category() {
                        Title = "Følelser",
                        Names = new List<Name>()
                    };

                    foreach (var name in emotionsList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             emotions.Names.Add(obj);
                        }                   
                    }

                    var liquorList = new List<string>() { "Gin", "Martini", "Vodka", "Whisky", "Whiskey", "Bailey", "Absint", "Absinth", "Rom", "Armagnac", "Champagne", "Riesling", "Mosel", "Vin", "Øl" };

                        var booze = new Category() {
                        Title = "Sprut",
                        Names = new List<Name>()
                    };

                    foreach (var name in liquorList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             booze.Names.Add(obj);
                        }                   
                    }

                    var titleList = new List<string>() { "Skipper", "Kong", "King", "Prince", "Queen", "Prince", "Princess", "Lord", "Sir", "Brormand", "Skat", "Fru", "Viking", "Bror", "Lillebror", "Lillemor", "Lillesøster", "Søster", "Baron", "Earl", "Jarl", "Sherif", "Duke", "Doc" };

                    var titles = new Category() {
                        Title = "Titler",
                        Names = new List<Name>()
                    };

                    foreach (var name in titleList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             obj.Prefix = true;
                             titles.Names.Add(obj);
                        }                   
                    }

                    var justFunnyList = new List<string>() { "Ninja", "Awesome", "Blær", "Dreng", "Charme", "Nitte", "Sok", "Piphat", "Pop", "Fe", "Gift", "Engel", "Offer", "Fessor" };

                    var funny = new Category() {
                        Title = "Lutter Sjove Navne",
                        Names = new List<Name>()
                    };

                    foreach (var name in justFunnyList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             funny.Names.Add(obj);
                        }                   
                    }

                    context.Categories.AddRange(
                        bible,
                        starWars,
                        starTrek,
                        tolkien,
                        harryPotter,
                        greekMyth,
                        norseMyth,
                        arabianNights,
                        vikings,
                        famous,
                        fictive,
                        instruments,
                        willies,
                        cities,
                        animals,
                        plants,
                        nature,
                        colours,
                        emotions,
                        booze,
                        titles,
                        funny
                    );
                }

                var funnyCombinations =  new List<string>() { "Lucky Luke", "Anders Sand", "Teddy Bjørn", "Dan Mark", "Mette Vuns" };

                // foreach (var name in funnyCombinations) {
                //     string[] nameStrings = name.Split(' ');

                //     var objs = context.Names.Where(x => x.Text == name).ToList();
                //     foreach (var obj in objs) {
                //         obj.Prefix = true;
                //         titles.Names.Add(obj);
                //     }                   
                // }

                #endregion

                context.SaveChanges();
            }
        }
    }
}
