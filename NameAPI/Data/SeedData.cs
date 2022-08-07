using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NameBandit.Data;
using NameBandit.Helpers;
using NameBandit.Models;

namespace NameBandit.Data {

    public class SeedData {


        public static void SeedDatabase(ApplicationDbContext context) {            
            if (context.Database.GetMigrations().Count() > 0 && context.Database.GetPendingMigrations().Count() == 0) {
                if (context.NameVibrationNumbers.Count() == 0) {
                    context.NameVibrationNumbers.AddRange(ScrapingHelper.GetVibrationMeanings());
                    context.SaveChanges();
                }
                
                if (context.Names.Count() == 0) {
                    var vibrations = context.NameVibrationNumbers.ToList();

                    var names = new List<Name>();

                    names = ScrapingHelper.ScapeNames(names, "http://www.urd.dk/fornavne/drenge.htm"); 
                    names = ScrapingHelper.ScapeNames(names, "http://www.urd.dk/fornavne/piger.htm", true);

                    names = ScrapingHelper.ScapeNames2(names);

                    foreach (var name in names) {
                        int vibration = CalculationHelper.CalculateNameVibration(name.Text);

                        var vibNumber = vibrations.Find(x => x.Vibration == vibration);

                        if (vibNumber != null) {
                            name.Vibration = vibNumber;
                        }                        
                        
                        // name.Description = ScrapingHelper.AddNameData(name);
                    }

                    context.Names.AddRange(names.Distinct().ToList());

                    context.SaveChanges();   
                }
             
                #region Categories

                if (context.NameCategories.Count() == 0) {
                    var bibleList = new List<string>() { "Adam", "Alexander", "Aleksander", "Benjamin", "Rakel", "Rachel", "Cornelius", "Dalila", "Dalilah", "David", "Goliat", 
                    "Goliath", "Elias", "Gabriel", "Immanuel", "Emmanuel", "Isak", "Sara", "Sarah", "Samson", "Jakob", "Jacob", "Jesus", "Johannes", "John", "Hans", "Jesus", 
                    "Jonatan", "Jonathan", "Lilith", "Lukas", "Lucas", "Markus", "Marcus", "Mattias", "Mathias", "Matias", "Matthæus", "Mikael", "Michael", "Noah", "Noa", 
                    "Silas", "Thomas", "Ada", "Elisabeth", "Elizabeth", "Elisabet", "Elizabet", "Ester", "Esther", "Eva", "Johanna", "Joanna", "Lea", "Leah", "Maria", "Mariah", 
                    "Rebekka", "Rebecca" };

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

                    var starWarsList = new List<string>() { "Ackbar", "Adi", "Amedda", "Amidala", "Anakin", "Antilles", "Arvel", "Ayla", "Bail", "Barriss", "Ben", "Beru", "Bib",
                    "Biggs", "Binks", "Boba", "Bolt", "Bossk", "C-3PO", "Calrissian", "Chewbacca", "Cliegg", "Cordé", "Crynyd", "Darklighter", "Darth", "Desilijic", "Dexter", 
                    "Dooku", "Dormé", "Dud", "Eeth", "Fett", "Finis", "Fisto", "Fortuna", "Gallia", "Gasgano", "Greedo", "Gregar", "Grievous", "Gunray", "Han", "Hill", "IG-88", 
                    "Jabba", "Jango", "Jar", "Jek", "Jettster", "Jinn", "Jocasta", "Kenobi", "Ki-Adi-Mundi", "Kit", "Koon", "Koth", "Lama", "Lando", "Lars", "Leia", "Lobot", 
                    "Luke", "Luminara", "Mace", "Mas", "Maul", "Medon", "Mon", "Moore", "Mothma", "Nass", "Nien", "Nu", "Nunb", "Nute", "Obi-Wan", "Offee", "Olié", "Organa", 
                    "Owen", "Padmé", "Palpatine", "Panaka", "Plo", "Poggle", "Poof", "Porkins", "Prestor", "Quadinaros", "Quarsh", "Qui-Gon", "R2-D2", "R4-P17", "R5-D4", "Ratts",
                    "Raymus", "Ric", "Roos", "Rugor", "Saesee", "San", "Sebulba", "Secura", "Shaak", "Shmi", "Skywalker", "Sly", "Solo", "Su", "Systri", "Tambor", "Tarfful", 
                    "Tarkin", "Tarpals", "Taun", "Ti", "Tiin", "Tion", "Tiure", "Tono", "Tyerel", "Typho", "Unduli", "Vader", "Valorum", "Warrick", "Wat", "Watto", "We", "Wedge",
                    "Wesell", "Whitesun", "Wicket", "Wilhuff", "Windu", "Yarael", "Yoda", "Zam" };

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

                    var starTrekList = new List<string>() { "Jean-Luc", "Kirk", "Spock", "Data" };

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
                    
                    var tolkienList = new List<string>() { "Aragorn", "Arwen", "Baggins", "Balin", "Bard", "Beorn", "Beren", "Bilbo", "Bombadil", "Boromir", "Brandybuck", 
                    "Celeborn", "Déagol", "Denethor", "Dís", "Elendil", "Elrond", "Elu", "Elwing", "Éomer", "Éowyn", "Eärendil", "Faramir", "Fëanor", "Felagund", "Fingolfin", 
                    "Finrod", "Finwë", "Frodo", "Galadriel", "Gamgee", "Gandalf", "Gil-galad", "Gimli", "Glorfindel", "Goldberry", "Gollum", "Gríma ", "Húrin", "Idril", "Indis",
                    "Isildur", "Kíli", "Legolass", "Lúthien", "Maedhros", "Melian", "Melkor", "Meriadoc", "Merry", "Míriel", "Morgoth", "Okensheild", "Peregrin", "Pippin", 
                    "Radagast", "Sam", "Samwise", "Saruman", "Sauron ", "Shelob", "Smaug", "Sméagol", "Théoden", "Thingol", "Thorin", "Thranduil", "Tom", "Took", "Treebeard", 
                    "Tuor", "Turambar", "Túrin", "Ungoliant", "Wormtongue" };

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
                    
                    var harryPotterList = new List<string>() { "Abbott", "Aberforth", "Alastor", "Albert", "Albus", "Alecto", "Alice", "Alicia", "Amelia", "Amos", "Amycus", 
                    "Andromeda", "Angelina", "Anthony", "Antioch", "Antonin", "Arabella", "Aragog", "Argus", "Ariana", "Arthur", "Astoria", "Augusta", "Augustus", "Aurora", 
                    "Bagman", "Bagshot", "Bane", "Barty", "Bathilda", "Beedle", "Bell", "Bellatrix", "Bertha", "Bill", "Binns", "Black", "Blaise", "Bloody", "Bob", "Bogrod", 
                    "Bones", "Boot", "Brown", "Bryce", "Buckbeak", "Bulstrode", "Burbage", "Cadmus", "Carrow", "Cattermole", "Cedric", "Chang", "Charity", "Charlie", "Cho", 
                    "Clearwater", "Colin", "Corban", "Cormac", "Cornelius", "Crabbe","Creevey", "Cresswell", "Crookshanks", "Crouch", "Cuthbert", "Dawlish", "Dean", "Dedalus", 
                    "Delacour", "Delphi", "Demelza", "Dennis", "Diggle", "Diggory", "Dirk", "Dobby", "Doge", "Dolohov", "Dolores", "Draco", "Dudley", "Dumbledore", "Dursley", 
                    "Edgecombe", "Eloise", "Elphias", "Emmeline", "Ernie", "Errol", "Fang", "Fat", "Fawkes", "Fenrir", "Figg", "Filch", "Filius", "Finch-Fletchley", "Finnigan", 
                    "Firenze", "Flamel", "Fletcher", "Fleur", "Flitwick", "Fluffy", "Frank", "Fred", "Friar", "Fudge", "Gabrielle", "Garrick", "Gaunt", "Gellert", "George", 
                    "Gilderoy", "Ginny", "Godric", "Goldstein", "Gornuk", "Goyle", "Graham", "Granger", "Grawp", "Great Aunt Muriel", "Greengrass", "Gregorovitch", "Gregory", 
                    "Greyback", "Grindelwald", "Griphook", "Griselda", "Grubbly-Plank", "Gryffindor", "Hagrid", "Hannah", "Harry", "Hedwig", "Helena", "Helga", "Hermione", 
                    "Hokey", "Hooch", "Hopkirk", "Horace", "Hufflepuff", "Hugo", "Ignotus", "Igor", "Irma", "James", "John", "Johnson", "Jordan", "Jorkins", "Justin", 
                    "Karkaroff", "Katie", "Kendra", "Kettleburn", "Kingsley", "Kreacher", "Krum", "Lavender", "Lee", "Lestrange", "Lily", "Lockhart", "Longbottom", "Lovegood", 
                    "Lucius", "Ludo", "Luna", "Lupin", "Macmillan", "Macnair", "Madam", "Madam Rosmerta", "Mafalda", "Magorian", "Malfoy", "Malkin", "Marchbanks", "Marge", 
                    "Marietta", "Marvolo", "Mary", "Maxime", "McGonagall", "McLaggen", "Merope", "Midgen", "Millicent", "Minerva", "Moaning", "Molly", "Montague", "Moody", 
                    "Morfin", "Mundungus", "Myrtle", "Myrtle", "Nagini", "Narcissa", "Neville", "Newt", "Nick", "Nicolas", "Nigellus", "Norbert", "Nott", "Nymphadora", "Ogden",
                    "Oliver", "Ollivander", "Olympe", "Padma", "Pansy", "Parkinson", "Parvati", "Patil", "Peeves", "Penelope", "Percival", "Percy", "Peter", "Pettigrew", 
                    "Petunia", "Peverell", "Phineas", "Pigwidgeon", "Pince", "Pius", "Podmore", "Pomfrey", "Pomona", "Poppy", "Potter", "Quirinus", "Quirrell", "Ravenclaw", 
                    "Reginald", "Remus", "Riddle", "Rita", "Robins", "Rolanda", "Romilda", "Ron", "Ronan", "Rookwood", "Rose", "Rowena", "Rowle", "Rubeus", "Rufus", "Runcorn", 
                    "Salazar", "Scabbers", "Scabior", "Scamander", "Scorpius", "Scrimgeour", "Seamus", "Septima", "Severus", "Shacklebolt", "Shunpike", "Silvanus", "Sinistra", 
                    "Sir Cadogan", "Sirius", "Skeeter", "Slughorn", "Slytherin", "Smith", "Snape", "Spinnet", "Sprout", "Stan", "Sturgis", "Susan", "Sybill", "Ted", "Terry", 
                    "Theodore", "Thicknesse", "Thomas", "Thomas", "Thorfinn", "Tom", "Tonks", "Travers", "Trelawney", "Trevor", "Twycross", "Umbridge", "Vance", "Vane", 
                    "Vector", "Vernon", "Viktor", "Vincent", "Voldemort", "Walden", "Warren", "Weasley", "Wilhelmina", "Wilkie", "Winky", "Wood", "Xenophilius", "Yaxley", 
                    "Zabini", "Zacharias" };

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

                    var greekMyhologyList = new List<string>() { "Zeus", "Muse", "Herkules", "Hercules", "Ares", "Afrodite", "Achilles", "Achilleus", "Hektor", "Hector", "Hera",
                    "Herakles", "Atena", "Atene", "Athena", "Athene", "Troja", "Trojan", "Troy", "Briseis", "Olymp", "Olympe", "Olympia" };

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
                    
                    var famousPeopleList = new List<string>() { "Elvis", "Clapton", "Armstrong", "Figo", "Pele", "Ronaldo", "Kaka", "Zlatan", "Napoleon", "Rocco", "Guti", 
                    "Nena", "Cæsar", "Dio", "Ozzy", "Dante", "Camus", "Chaucer", "Cher", "Enya" };

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

                    var fictivePeopleList = new List<string>() { "Asterix", "Barbie", "Barnaby", "Obelix", "Sherlock", "Snoopy", "Tintin", "Rocky", "Dolph", "Pixieline", 
                    "Maverick", "Garfield", "Zorro", "Tarzan", "Eponine", "Gaston", "Belle", "Willow", "Sorcha" };

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
                    
                    var instrumentsList = new List<string>() { "Cello", "Gitar", "Bas", "Oboe", "Obo" };

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

                    var citiesList = new List<string>() { "Frejlev", "London", "Berlin", "York", "Nice", "Tokyo", "Madrid", "Valencia", "Sevilla", "Roma", "Milan", "Verona", 
                    "Lyon", "Guldborg", "Preston", "Boston", "Kingston", "Carlisle", "Lincoln", "Cleveland", "Charlton", "Preston" };

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

                    var animalsList = new List<string>() { "Bjørn", "Ulv", "Cobra", "Kobra", "Haj", "Havand", "Musling", "Panda", "Kamel", "Løve", "Tiger", "Panthera", "Lion", 
                    "Svane", "Spurv", "Ravn", "Stærkodder", "Gekko", "Snake", "Moose" };

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
                    
                    var plantsList = new List<string>() { "Hyben", "Mynte", "Solsikke", "Birk", "Amaryllis", "Hyachint", "Eg", "Viol", "Rose", "Oregano", "Timian", "Persille", 
                    "Lavendel", "Kløver", "Rosmarin", "Sweetpea" };

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

                    var naturesList = new List<string>() { "Sne", "Sommer", "Sol", "Sea", "Sten", "Sun", "Moon", "Cliff", "Rock", "Stone" };

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
                    
                    var coloursList = new List<string>() { "Gul", "Violet", "Azur", "Blå", "Brun", "Cyan", "Grå", "Grøn", "Hvid", "Indigo", "Lilla", "Lyserød", "Pink", "Okker", 
                    "Purpur", "Rosa", "Rød", "Sort", "Umbra", "Lime", "Lyseblå", "Lysegrøn", "Orange" };

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

                    var liquorList = new List<string>() { "Gin", "Martini", "Vodka", "Whisky", "Whiskey", "Bailey", "Absint", "Absinth", "Rom", "Armagnac", "Champagne", 
                    "Riesling", "Mosel", "Vin", "Øl" };

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

                    var titleList = new List<string>() { "Admiral", "Skipper", "Kong", "King", "Prince", "Queen", "Prince", "Princess", "Lord", "Sir", "Brormand", "Farmand", 
                    "Skat", "Fru", "Viking", "Bror", "Lillebror", "Lillemor", "Lillesøster", "Søster", "Baron", "Earl", "Jarl", "Sherif", "Duke", "Doc", "Prior", "Boy", "Son", 
                    "Kid", "Bonde", "Lady", "Papa", "Mama", "Bandit" };

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

                    var surnameList = new List<string>() { "Addison", "Adison", "Ainsley", "Allison", "Allyson", "Alton", "Anaton", "Anson", "Anuson", "Ariston", "Ashley", 
                    "Ashton", "Aston", "Ayrton", "Barnaby", "Barrington", "Benson", "Bentley", "Berton", "Bibson", "Boston", "Bradley", "Brenton", "Britton", "Bryson", 
                    "Carlson", "Carlton", "Carrington", "Carton", "Charlton", "Chayton", "Clapton", "Clayson", "Clayton", "Clinton", "Dalton", "Davinson", "Dickson", "Driton", 
                    "Easton", "Edison", "Edson", "Elton", "Emerson", "Eton", "Faton", "Gaston", "Grayson", "Greyson", "Hadley", "Hamilton", "Harrison", "Helton", "Hilton", 
                    "Hudson", "Huntley", "Huxley", "Jackson", "Jayson", "Jeferson", "Jefferson", "Jeton", "Johnson", "Jones", "Karlanton", "Karlton", "Keaton", "Kenton", 
                    "Kingsley", "Kingston", "Kleton", "Leighton", "Lesley", "Leyton", "Lindley", "Linton", "Maddison", "Madison", "Mailey", "Manley", "Marley", "Mason", 
                    "Meriton", "Milton", "Morton", "Maarlley", "Naton", "Nelson", "Newton", "Norton", "Oakley", "Orson", "Paisley", "Paxton", "Payton", "Peyton", "Presley", 
                    "Preston", "Printon", "Quinton", "Rasmusbailey", "Rickson", "Riley", "Ripley", "Robinson", "Rockson", "Ronson", "Royston", "Ryley", "Seeley", "Shelley", 
                    "Sheridan", "Soley", "Souley", "Stanley", "Sutton", "Tilley", "Triston", "Triton", "Tyson", "Valley", "Valton", "Vilton", "Vinston", "Waley", "Washington", 
                    "Wesley", "Weston", "Wiley", "Willey", "Wilson", "Wilton", "Winston" };

                    var surnames = new Category() {
                        Title = "Efternavne",
                        Names = new List<Name>()
                    };

                    foreach (var name in surnameList) {
                        var objs = context.Names.Where(x => x.Text == name).ToList();
                        foreach (var obj in objs) {
                             obj.Suffix = true;
                             titles.Names.Add(obj);
                        }                   
                    }

                    var justFunnyList = new List<string>() { "Ninja", "Awesome", "Blær", "Dreng", "Charme", "Nitte", "Sok", "Piphat", "Pop", "Fe", "Gift", "Engel", "Offer", 
                    "Fessor", "Girl", "Guf", "Adduha", "Nisse", "Smiley" };

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

                    var funnyCombinations =  new List<string>() { "Lucky Luke", "Anders Sand", "Teddy Bjørn", "Dan Mark", "Mette Vuns", "Awesome Viking",  "Awesome Willy", 
                    "Awesome Man", "Nice Viking", "Nice Willy", "Nice Man", "Rocket Man", "Bunny Man", "Rocket Girl", "Nice Girl", "Bunny Girl", "Lucky Man", "Strange Man", 
                    "The Man", "Strange Viking", "Strange Willy", "The Willy", "The Viking", "Gordon Gekko", "Tom Barnaby", "John Barnaby", "Nice Pixie", "Pixie Girl", 
                    "Rocket Pixie", "Pixie Bunny", "New York", "Ninja Girl", "Peter Pan", "Snake Eyess", "Joe Dalton", "Ma Dalton", "Ice Man", "John Connor", "Pixie Ninja", 
                    "Sir Lancelot",  "Awesome Boy", "Nice Boy", "Strange Boy", "King Kong", "Indiana Jones", "Wolf Man", "Baba Papa", "Bjørne Bandit", "Fin Raziel", 
                    "Bonny Lass", "Bonnie Girl" };
                
                    foreach (var name in funnyCombinations) {
                        string[] parts = name.Split(' ');

                        var funnyList = new List<NamePrio>();

                        for (int i = 0; i < parts.Count(); i++) {
                            var part = parts[i];
                            var ibbermand = context.Names.Where(x => x.Text == part).FirstOrDefault();

                            if (ibbermand != null) {
                                if (i != 0) {
                                    ibbermand.Suffix = true;
                                } 

                                 funnyList.Add(new NamePrio() {
                                    Name = ibbermand,
                                    Prio = i + 1
                                });
                            }
                        }
                        
                        if (funnyList.Count() == parts.Count()) {
                            var combo = new NameCombo() {
                                Id = 0,
                                Names = funnyList,
                                Category = funny
                            };

                            context.NameCombinations.Add(combo); 
                        }              
                    }

                    var famousCombinations =  new List<string>() { "Michael Jackson", "John Lennon", "Scott McKenzie", "George Harrison", "Ringo Star", "Mike Tyson", 
                    "Peter Gabriel", "Phil Collins", "The King", "The Rock", "Bryan Wilson", "Carl Wilson", "Mike Love", "Dennis Wilson", "Al Jardine", "David Livingstone", 
                    "Henry Morton Stanley", "Gordon Ramsey", "Eric Clapton", "Napoleon Bonaparte", "Luis Figo", "Cristiano Ronaldo", "Neil Armstrong", "Lance Armstrong", 
                    "Louis Armstrong", "Edwin Aldrin", "Michael Collins", "Stephen King", "Victor Hugo", "James Dean", "George Michael", "Elton John", "Julius Cæsar", 
                    "Immanuel Kant", "George Washington", "Ralph Waldo Emerson", "Karl Marx", "Ronald Reagan", "Bill Clinton", "Thomas Jefferson", "John Adams", "Tony Adams", 
                    "James Madison", "Marilyn Monroe", "James Monroe", "Andrew Jackson", "William Harrison", "John Tyler", "John Polk", "Zachary Taylor", "Millard Fillmore", 
                    "Franklin Pierce", "James Buchanan", "Abraham Lincoln", "Andrew Johnson", "Ulysses Grant", "Rutherford Hayes", "James Garfield", "Chester Arthur", 
                    "Grover Cleveland", "Benjamin Harrison", "William McKinley", "Theodore Roosevelt", "William Taft", "Woodrow Wilson", "Warren Harding", "Calvin Coolidge", 
                    "Herbert Hoover", "Franklin Roosevelt", "Harry Truman", "Dwight Eisenhower", "John Kennedy", "Lyndon Johnson", "Richard Nixon", "Gerald Ford", 
                    "Jimmy Carter", "George Bush", "Barack Obama", "Donald Trump", "Joe Biden", "Thomas Hardy", "Ian Fleming", "Charles Dickens", "Mary Shelley", "Lord Nelson", 
                    "Charles Mason", "Jeremiah Dixon", "Thomas Edison", "Boy George", "David Bowie", "Elvis Presley", "Bob Paisley", "Miley Cyrus", "Stewie Wonder", 
                    "Michael Owen", "Salman Rushdi", "Sofie Bonde", "Von Trier", "Kong Ramses", "King Solomon", "Papa Bue", "Mama Cass", "Steven Gerrard", "Smokey Robinson", 
                    "Hugh Grant", "Prince John", "King Richard", "Queen Mary", "Queen Elizabeth", "Prince Charles", "Prince William", "Princess Diana", "Prince Harry", 
                    "Bonny Tyler", "Lewis Carol", "Don Johnson", "Tom Cruise", "Benjamin Franklin", "Demi More", "Michael Douglas", "Bonnie Prince Charles", "Paris Hilton",
                    "Bryan Adams", "Rod Stewart", "Hans Kirk", "Bille August", "Jack London", "John Milton", "Tom Hanks", "James Barrie", "Robert Scott", "Harper Lee", 
                    "Herman Melville", "Antonio Vivaldi", "Dina Jewel", "Peter North", "Ron Jeremy", "Evan Stone", "Michael Keaton", "Michael Fox", "Bob Marley", "Ruby Dee",
                    "Jim Davis", "Viven Leigh", "Samantha Bee", "Margaret Cho", "Elizabeth Taylor", "Lucy Liu", "Agatha Christie", "George Eliot", "Marie Curie", "Carol King",
                    "Billie King", "Brune Mars", "Bent Fabricius Bjerre", "Nat King Cole", "Donna Summer", "Hanne Boel", "Anne Linett", "Cliff Richard", "Keith Richard", 
                    "Mick Jagger", "Peter Andre", "Eddy Grant", "Billy Idol", "Bon Jovi" };
                
                    foreach (var name in famousCombinations) {
                        string[] parts = name.Split(' ');

                        var famousList = new List<NamePrio>();

                        for (int i = 0; i < parts.Count(); i++) {
                            var part = parts[i];
                            var ibbermand = context.Names.Where(x => x.Text == part).FirstOrDefault();

                            if (ibbermand != null) {
                                if (i != 0) {
                                    ibbermand.Suffix = true;
                                } 

                                famousList.Add(new NamePrio() {
                                    Name = ibbermand,
                                    Prio = i + 1
                                });
                            }
                        }
                        
                        if (famousList.Count() == parts.Count()) {
                            var combo = new NameCombo() {
                                Id = 0,
                                Names = famousList,
                                Category = famous
                            };

                            context.NameCombinations.Add(combo); 
                        }              
                    }

                    context.NameCategories.AddRange(
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
                        surnames,
                        funny
                    );                    
                }

                #endregion

                context.SaveChanges();
            }
        }
    }
}

