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
                    var bibleList = new List<string>() { "Abaddon", "Abagtha", "Abana", "Abarim", "Abba", "Abda", "Abdeel", "Abdi", "Abdiel", "Abdon", "Abednego", "Abel", 
                    "Abez", "Abi", "Abiasaph", "Abiathar", "Abib", "Abida", "Abidah", "Abidan", "Abieezer", "Abiel", "Abiezer", "Abigail", "Abihail", "Abihu", "Abihud", 
                    "Abijah", "Abijam",  "Abilene", "Abimael", "Abimelech", "Abinadab", "Abinoam", "Abiram", "Abishag", "Abishai", "Abishalom", "Abishua", "Abishur", "Abital", 
                    "Abitub", "Abiud", "Abiyyah", "Abner", "Abraham", "Abram", "Absalom", "Abubus", "Accad", "Accho", "Aceldama", "Achab", "Achaia", "Achaicus", "Achan", 
                    "Achar", "Achaz", "Achbor", "Achim", "Achish", "Achmetha", "Achor", "Achsah", "Achshaph", "Achzib", "Ada", "Adadah", "Adah", "Adaiyyah", "Adalia", "Adam", 
                    "Adamah", "Adami", "Adar", "Adbeel", "Addar", "Addi", "Addin", "Addon", "Adiel", "Adin", "Adina", "Adithaim", "Adlai", "Admah", "Admatha", "Admin", "Adna", 
                    "Adnah", "Adoni", "Adonibezek", "Adonijah", "Adonikam", "Adoniram", "Adoraim", "Adoram", "Adrammelech", "Adramyttium", "Adria", "Adriel", "Aduel", "Adullam",
                     "Adummim", "Aedias", "Aeneas", "Aenon", "Aesora", "Agabus", "Agag", "Agagite", "Agar", "Agee", "Aggaba", "Agia", "Agrippa", "Agur", "Ahab", "Aharah", 
                     "Aharhel", "Ahasbai", "Ahasuerus", "Ahava", "Ahaz", "Ahaziyyah", "Ahi", "Ahiah", "Ahiam", "Ahian", "Ahiezer", "Ahihud", "Ahijah", "Ahikam", "Ahilud", 
                     "Ahiman", "Ahimelech", "Ahimoth", "Ahimaaz", "Ahinadab", "Ahinoam", "Ahio", "Ahira", "Ahiram", "Ahisamach", "Ahishahar", "Ahishar", "Ahithophel", "Ahitub", 
                     "Ahlab", "Ahlai", "Ahoah", "Aholah", "Aholiab", "Aholibah", "Aholibamah", "Ahumai", "Ahuzam", "Ahuzzath", "Ai", "Aiah", "Aiath", "Aijeleth", "Ain", 
                     "Ajalon", "Akeldama", "Akkad", "Akkub", "Akrabbim", "Alammelech", "Albon", "Aleksander", "Alemeth", "Alexander", "Alian", "Alleluyah", "Allon", "Almodad", 
                     "Almon", "Alpheus", "Alush", "Alvah", "Amad", "Amal", "Amalek", "Aman", "Amana", "Amariyyah", "Amasa", "Amashai", "Amasia", "Amaziah", "Ami", "Aminadab", 
                     "Amittai", "Ammah", "Ammi", "Ammiel", "Ammihud", "Amminadab", "Ammishaddai", "Ammizabad", "Ammon", "Amnon", "Amok", "Amon", "Amorite", "Amos", "Amoz", 
                     "Amplias", "Amram", "Amraphel", "Amzi", "Anab", "Anah", "Anaharath", "Anaiah", "Anak", "Anamim", "Anammelech", "Anani", "Ananias", "Anathema", "Anathoth", 
                     "Andrew", "Andronicus", "Anem", "Aner", "Aniam", "Anim", "Anna", "Annas", "Antichrist", "Antioch", "Antipas", "Antipatris", "Antothijah", "Anub", "Apelles",
                     "Apharsathchites", "Aphek", "Aphekah", "Aphiah", "Aphik", "Apocalypse", "Apocrypha", "Apollonia", "Apollonius", "Apollos", "Apollyon", "Appaim", "Apphia", 
                     "Aquila", "Ar", "Ara", "Arab", "Arabia", "Arad", "Arah", "Aram", "Aram¨", "Aran", "Ararat", "Araunah", "Arba", "Archelaus", "Archippus", "Arcturus", "Ard", 
                     "Ardon", "Areli", "Areopagus", "Aretas", "Argob", "Ariel", "Arim", "Arimathea", "Arioch", "Aristarchus", "Aristobulus", "Armageddon", "Arnon", "Aven", 
                     "Azmaveth", "Babel", "Babylon", "Baca", "Bachuth", "Bahurim", "Bajith", "Bakbakkar", "Bakbuk", "Bakbukiah", "Baladan", "Balak", "Ball", "Balaam", "Bamah", 
                     "Barabbas", "Barachel", "Barachias", "Barah", "Barak", "Barjesus", "Barjona", "Barnabas", "Barsabas", "Bartholomew", "Bartimeus", "Baruch", "Barzillai", 
                     "Bashan", "Bashemath", "Bathsheba", "Bathsuha", "Baz", "Bealiah", "Bealoth", "Bebai", "Becher", "Bechorath", "Bedad", "Bedaiah", "Bedan", "Beeliada", 
                     "Beelzebub", "Beer", "Beera", "Beerelim", "Beeri", "Beeroth", "Beersheba", "Behemoth", "Bekah", "Belah", "Belial", "Belshazzar", "Belteshazzar", "Ben", 
                     "Benaiah", "Beneberak", "Benhadad", "Benhail", "Benhanan", "Benimi", "Benjamin", "Beno", "Benob", "Benoni", "Benoth", "Benzoheth", "Beon", "Beor", "Bera", 
                     "Berachah", "Berachiah", "Beraiah", "Berea", "Bered", "Beri", "Beriah", "Berith", "Bernice", "Berodach", "Berothai", "Berothath", "Besai", "Beseth", 
                     "Besodeiah", "Besor", "Betah", "Beten", "Beth", "Bethabara", "Bethanath", "Bethany", "Betharabah", "Beth-aram", "Betharbel", "Bethel", "Bethemek", "Bether",
                     "Bethesda", "Bethphage", "Bethsaida", "Bethshan", "Bethuel", "Betonim", "Beulah", "Bezai,", "Bezaleel", "Bezek", "Bezer", "Bichri", "Bidkar", "Bigthan", 
                     "Bigvai", "Bildad", "Bileam", "Bilgah", "Bilhah", "Bilshan", "Binea", "Binnui", "Birei", "Birsha,", "Bishlam", "Bithiah", "Bithron", "Bithynia", 
                     "Bizjothjas", "Blastus", "Boanerges", "Boaz", "Bocheru", "Bochim", "Bohan", "Boskath", "Boson", "Boznai", "Bozrah", "Bukki", "Bukkiah", "Bul", "Bunah", 
                     "Bunni", "Buz", "Buzi", "Baal", "Baalah", "Baalath", "Baali", "Baalim", "Baalis", "Baalmeon", "Baana", "Baanah", "Baara", "Baaseiah", "Baasha", "Cabbon", 
                     "Cabul", "Caesar", "Cain", "Cainan", "Caiphas", "Calah", "Calcol", "Caleb", "Calem", "Calneh", "Calno", "Calvary", "Camon", "Cana", "Candace", "Canaan", 
                     "Capernaum", "Caphtor", "Cappadocia", "Car", "Carcas", "Careah", "Carmel", "Carmi", "Carpus", "Carshena", "Casiphia", "Casleu", "Casluhim", "Castiel", 
                     "Cedron", "Cenchrea", "Cephas", "Cesar", "Cesia", "Chalcol", "Charchemish", "Charran", "Chebar", "Chedorlaomer", "Chelal", "Chelluh", "Chelub", "Chelubai", 
                     "Chemarims", "Chemosh", "Chenani", "Chenaniah", "Chenaanah", "Chephirah", "Cheramim", "Cheran", "Cherith", "Chesed", "Chesil", "Chesulloth", "Chidon", 
                     "Chiliab", "Chilion", "Chilmad", "Chimham", "Chios", "Chisleu", "Chislon", "Chisloth", "Chittem", "Chloe", "Chorath", "Chorazin", "Chozeba", "Christ", 
                     "Christian", "Chun", "Chushan", "Chuza", "Cilicia", "Cis", "Cisleu", "Clauda", "Claudia", "Claudius", "Clement", "Cleophas", "Cnidus", "Colhozeh", "Colosse",
                     "Coniah", "Coos", "Corinth", "Cornelius", "Cosam", "Coz", "Crescens", "Crete", "Crispus", "cumi", "Cush", "Cushan", "Cuth", "Cyprus", "Cyrene", "Cyrenius", 
                     "Cyril", "Cyrus", "Dabareh", "Dabbasheth", "Daberath", "Dagon", "Dalaiah", "Dalila", "Dalilah", "Dalmanutha", "Dalmatia", "Dalphon", "Damaris", "Damascus", 
                     "Damien", "Dammim", "Dammin", "Dan", "Daniel", "Dannah", "Dara", "Darda", "Darius", "Darkon", "Dathan", "David", "Debir", "Deborah", "Decapolis", "Dedan", 
                     "Dedanim", "Dekar", "Delaiah", "Delilah", "Demas", "Demetrius", "Derbe", "Deuel", "Deuteronomy", "Diana", "Diblah", "Diblaim", "Diblath", "Diblathaim", 
                     "Dibon", "Dibri", "Didymus", "Diklah", "Dilean,", "Dimon", "Dimonah", "Dinah", "Dinhabah", "Dionysius", "Diotrephes", "Dishan", "Dishon", "Dizahab", "Dodai,",
                     "Dodavah", "Dodo", "Doeg", "Dophkah", "Dor", "Dorcas", "Dothan", "Drusilla", "Dumah", "Dura", "Ebal", "Ebed", "Eber", "Ebiasaph", "Ebronah", "Ecclesiastes", 
                     "Ecclesiasticus", "Ed", "Eden", "Eder", "Edom", "Edrei", "Eglah", "Eglaim", "Eglon", "Egypt", "Ehi", "Ehud", "Eker", "Ekron", "El", "Eladah", "Elah", "Elam", 
                     "Elasah", "Elath", "Eldad", "Eldaah", "Elead", "Elealeh", "Eleasah", "Eleazar", "Eleph", "Elhanan", "Eli", "Eliab", "Eliada", "Eliah", "Eliahba", "Eliakim", 
                     "Eliam", "Elias", "Eliasaph", "Eliashib", "Eliathah", "Elidad", "Eliel", "Elienai", "Eliezer", "Elihoreph", "Elihu", "Elijah", "Elika", "Elim", "Elimelech", 
                     "Elioenai", "Eliphal", "Eliphaz", "Eliphelet", "Elisabet", "Elisabeth", "Elisha", "Elishah", "Elishama", "Elishaphat", "Elisheba", "Elishua", "Eliud", 
                     "Elizabet", "Elizabeth", "Elizur", "Elkanah", "Elkoshite", "Ellasar", "Elm", "Elmodam", "Elnathan", "Elnaam", "Elohe", "Elon", "Elpalet", "Elpaal", "Eltekeh",
                     "Eltolad", "Elul", "Eluzai", "Elymas", "Elzabad", "Elzaphan", "Emims", "Emmanuel", "Emmaus", "Emmor", "En", "Enan", "Eneas", "Enoch", "Enon", "Enos", 
                     "Epaphras", "Epaphroditus", "Epenetus", "Ephah", "Epher", "Ephes", "Ephesus", "Ephlal", "Ephphatha", "Ephraim", "Ephratah", "Ephron", "Epicurean", "Er", 
                     "Eran", "Erastus", "Eri", "Esaias", "Esar", "Esau", "Esek", "Eshban", "Eshbaal", "Eshcol", "Eshean", "Eshek", "Eshkalon", "Eshtaol", "Eshtemoa", "Esli", 
                     "Esrom", "Ester", "Esther", "Etam", "Etham", "Ethan", "Ethanim", "Ethbaal", "Ethiopia", "Ethnan", "Ethni", "Eubulus", "Eunice", "Euodias", "Euphrates", 
                     "Eutychus", "Eva", "Eve", "Evi", "Evil", "Exodus", "Ezal", "Ezbon", "Ezekiel", "Ezel", "Ezem", "ezer", "Ezion", "Ezra", "Ezri", "Felix", "Festus", 
                     "Fortunatus", "Gabbai", "Gabbatha", "Gabriel", "Gad", "Gadarenes", "Gaddah", "Gaddi", "Gaddiel", "Gader", "Gaius", "Galal", "Galatia", "Galeed", "Galilee", 
                     "Gallim", "Gallio", "Gamaliel", "Gammadims", "Gamul", "Gannim", "Gareb", "Garmites", "Gatam", "Gath", "Gaza", "Gazer", "Gazez", "Gazzam", "Geba", "Gebal", 
                     "Geber", "Gebim", "Gedaliah", "Geder", "Gederothaim", "Gedi", "Gehazi", "Geliloth", "Gemalli", "Gemariah", "Genesis", "Gennesaret", "Genubath", "Gera", 
                     "Gerar", "Gergesenes", "Gerizim", "Gershom", "Gershon", "Geshur", "Geshuri", "Gether", "Gethsemane", "Geuel", "Gezer", "Giah", "Gibbar", "Gibbethon", 
                     "Gibeah", "Gibeon", "Giddel", "Gideon", "Gideoni", "Gihon", "Gilalai", "Gilboa", "Gilead", "Gilgal", "Giloh", "Gimzo", "Ginath", "Ginnetho", "Girgashite", 
                     "Gispa", "Gittah", "Gittaim", "Gittites", "Goath", "Gob", "Gog", "Golan", "Golgotha", "Goliat", "Goliath", "Gomer", "Gomorrah", "Goshen", "Gozan", "Gudgodah",
                      "Guni", "Gur", "Gaal", "Gaash", "Habaiah", "Habakkuk", "Habazziniah", "Habor", "Haccerem", "Hachaliah", "Hachilah", "Hachmoni", "Hadad", "Hadadezer", 
                      "Hadadrimmon", "Hadar", "Hadarezer", "Hadashah", "Hadassah", "Hadattah", "Haddah", "Haddon", "Hades", "Hadlai", "Hadoram", "Hadrach", "Hagab", "Hagabah", 
                      "Hagar", "Haggai", "Haggeri", "Haggiah", "Haggith", "Hagidgad", "hahiroth", "Hai", "Hakkatan", "Hakkore", "Hakkoz", "Hakupha", "Halah", "Halak", "Halhul", 
                      "Hali", "Hallelujah", "Hallohesh", "Ham", "Haman", "Hamath", "Hammahlekoth", "Hammedatha", "Hammelech", "Hammoleketh", "Hammon", "Hamon", "Hamonah", 
                      "Hamor", "Hamul", "Hamutal", "Hanameel", "Hanan", "Hananeel", "Hanani", "Hananiah", "Hanes", "Haniel", "Hannah", "Hannathon", "Hanniel", "Hanoch", "Hans", 
                      "Hanun", "Hapharaim", "Happuch", "Hara", "Haradah", "Haran", "Haraseth", "Harbonah", "Hareph", "Harhaiah", "Harhas", "Harhur", "Harim", "Harnepher", 
                      "Harod", "Harosheth", "Harran", "Harsha", "Harum", "Harumaph", "Haruphite", "Haruz", "Hasadiah", "Hash", "Hashabiah", "Hashabnah", "Hashem", "Hashub", 
                      "Hashubah", "Hashum", "Hashupha", "Hasrah", "Hatach", "Hathath", "Hatita", "Hatticon", "Hattil", "Hattill", "Hattush", "Hattaavah", "Hauran", "Havilah", 
                      "Havoth", "Hazael", "Hazaiah", "Hazar", "Hazarmaveth", "Hazeroth", "Hazezon", "Hazo", "Hazor", "Hazzelelponi", "Hazzurim", "Heber", "Hebrews", "Hebron", 
                      "Hegai", "Hege", "Helam", "Helbah", "Helbon", "Heldai", "Heleb", "Heled", "Helek", "Helem", "Heleph", "Helez", "Heli", "Helkai", "Helkath", "Helon", "Heman", 
                      "Hen", "Hena", "Henadad", "Henoch", "Hepher", "Hephzibah", "Heres", "Heresh", "Hermas", "Hermogenes", "Hermon", "Herod", "Herodion", "Hesed", "Heshbon", 
                      "Heshmon", "Heth", "Hethlon", "Hezekiah", "Hezir", "Hezrai", "Hezron", "Hiddai", "Hiel", "Hierapolis", "Hilen", "Hilkiah", "Hillel", "Hinnom", "Hirah", 
                      "Hiram", "Hittite", "Hivites", "Hizkijah", "Hobab", "Hobah", "Hod", "Hodaiah", "Hodaviah", "Hodesh", "Hoglah", "Hoham", "Holon", "Homam", "Hophni", "Hophra", 
                      "Hor", "Horeb", "Horem", "Hori", "Horims", "Horinites", "Hormah", "Horon", "Horonaim", "Hosah", "Hosanna", "Hosea", "Hoshaiah", "Hoshama", "Hotham", "Hothir", 
                      "Hukkok", "Hul", "Huldah", "Hupham", "Huppim", "Hur", "Huram", "Huri", "Hushah", "Hushai", "Hushathite", "Huz", "Huzoth", "Huzzab", "Hymeneus", "Haahashtari", 
                      "Ibhar", "Ibleam", "Ibneiah", "Ibnijah", "Ibri", "Ibsam", "Ibzan", "Ichabod", "Iconium", "Idalah", "Idbash", "Iddo", "Idumea", "Igal", "Igdaliah", "Igeal", 
                      "Iim", "Ije-abarim", "Ijon", "Ikkesh", "Ilai", "Illyricum", "Imla", "Imlah", "Immanuel", "Immanuel", "Immer", "Imna", "Imnah", "Imrah", "Imri", "India", 
                      "Iphdeiah", "Ir", "Ira", "Irad", "Iram", "Iri", "Irijah", "Irpeel", "Iru", "Isaiah", "Isak", "Iscah", "Iscariot", "Ishbah", "Ishbak", "Ishbi", "Ishbosheth", 
                      "Ishi", "Ishiah", "Ishma", "Ishmael", "Ishmaiah", "Ishmerai", "Ishod", "Ishpan", "Ishtob", "Ishuah", "Ishui", "Ishvah", "Ishvi", "Ismaiah", "Ispah", "Israel", 
                      "Issachar", "Isshiah", "Isshijah", "Isuah", "Isui", "Isaac", "Italy", "Ithai", "Ithamar", "Ithiel", "Ithmah", "Ithra", "Ithran", "Ithream", "Ittah", "Ittai", 
                      "Iturea", "Ivah", "Iye", "Izehar", "Izhar", "Izrahiah", "Izri", "Izziah", "Jabal", "Jabbok", "Jabesh", "Jabez", "Jabin", "Jabneel", "Jachan", "Jachin", "Jacob",
                      "Jada", "Jadau", "Jaddua", "Jadon", "Jael", "Jagur", "Jah", "Jahath", "Jahaz", "Jahaziah", "Jahaziel", "Jahdai", "Jahdiel", "Jahdo", "Jahleel", "Jahmai", 
                      "Jahzeel", "Jahzerah", "Jair", "Jairus", "Jakan", "Jakeh", "Jakim", "Jakob", "Jalon", "Jambres", "James", "Jamin", "Jamlech", "Janna", "Janoah", "Janum", 
                      "Japhet", "Japheth", "Japhia,", "Japhlet", "Japho", "Jarah", "Jareb", "Jared", "Jaresiah", "Jarib", "Jarmuth", "Jasher", "Jashobeam", "jashub", "Jasiel", 
                      "Jason", "Jathniel", "Jattir", "Javan", "Jazer", "Jaziz", "Jearim", "Jeaterai", "Jeberechiah", "Jebus", "Jebusi", "Jecamiah", "Jecoliah", "Jeconiah,", "Jed", 
                      "Jedaiah", "Jediael", "Jedidah", "Jedidiah", "Jeduthun", "Jeezer", "Jegar", "Jehaleleel", "Jehdeiah", "Jehezekel", "Jehiah", "Jehizkiah", "Jehoadah", 
                      "Jehoaddan", "Jehoahaz", "Jehoash", "Jehohanan", "Jehoiachin", "Jehoiada", "Jehoiakim", "Jehoiarib", "Jehoida", "Jehonadab", "Jehonathan", "Jehoram", 
                      "Jehoshaphat", "Jehosheba", "Jehoshua", "Jehovah", "Jehozabad", "Jehozadak", "Jehu", "Jehubbah", "Jehucal", "Jehud", "Jehudijah", "Jehush", "Jekabzeel", 
                      "Jekameam", "Jekamiah", "Jekuthiel", "Jemima", "Jemuel", "Jephthah", "Jephunneh", "Jerah", "Jerahmeel", "Jered", "Jeremai", "Jeremiah", "Jeremoth", "Jeriah", 
                      "Jericho", "Jeriel", "Jerijah", "Jerimoth", "Jerioth", "Jeroboam", "Jeroham", "Jerubbesheth", "Jerubbaal", "Jeruel", "Jerusalem", "Jerusha", "Jesaiah", 
                      "Jeshebeab", "Jesher", "Jeshimon", "Jeshishai", "Jeshohaia", "Jeshua", "Jeshurun", "Jesiah", "Jesimiel", "Jesse", "Jesui", "Jesus", "Jether", "Jetheth", 
                      "Jethlah", "Jethro", "Jetur", "Jeuel", "Jeush", "Jew", "Jezaniah", "Jezebel", "Jezer", "Jeziah", "Jezoar", "Jezrahiah", "Jezreel", "Jibsam", "Jidlaph", 
                      "Jimnah", "Jiphtah", "Jiphtah-el", "Jireh", "Joab", "Joachim", "Joah", "Joahaz", "Joanna", "Joash", "Joatham", "Job", "Jobab", "Jochebed", "Joed", "Joel", 
                      "Joelah", "Joezer", "Jogbehah", "Jogli", "Joha", "Johanan", "Johanna", "Johannes", "John", "Joiarib", "Jokdeam", "Jokim", "Jokmeam", "Jokneam", "Jokshan", 
                      "Joktan", "Jonadab", "Jonah", "Jonan", "Jonatan", "Jonathan", "Joppa", "Jorah", "Joram", "Jordan", "Jorim", "Josabad", "Josaphat", "Jose", "Joseph", "Joses", 
                      "Joshah", "Joshaviah", "Joshbekashah", "Joshua", "Joshva", "Josiah", "Josibiah", "Josiphiah", "Josva", "Jotham", "Jozabad", "Jozachar", "Jubal", "Jucal", 
                      "Judaea", "Judah", "Judas", "Jude", "Judith", "Julia", "Julius", "Junia", "Jushab", "Justus", "Juttah", "Jaakan", "Jaala", "Jaalam", "Jaanai", "Jaasau", 
                      "Jaasiel", "Jaasu", "Jaazaniah", "Jaaziah", "Jaaziel", "Kabzeel", "Kadesh", "Kadmiel", "Kadmonites", "Kallai", "Kamon", "Kanah", "Kareah", "Karkor", "Karkaa", 
                      "Kartah", "Kazin", "Kedar", "Kedemah", "Kedemoth", "Keilah", "Kelaiah", "Kelita", "Kemuel", "Kenah", "Kenan", "Kenaz", "Kenites", "Kenizzites", "Keren", 
                      "Kerioth", "Keros", "Keturah", "Kezia", "Keziz", "Kibroth", "Kibzaim", "Kidron", "Kinah", "Kir", "Kiriath", "Kiriathaim", "Kirioth", "Kirjath", "Kirjathaim", 
                      "Kish", "Kishi", "Kishion", "Kishon", "Kithlish", "Kitron", "Kittim", "Koa", "Kohath", "Kolaiah", "Korah", "Kushaiah", "Laban", "Labana", "Lachish", "Lael", 
                      "Lahad", "Lahai", "Lahairoi", "Lahmi", "Laish", "Lakum", "Lamech", "Laodicea", "Lapidoth", "Lasea", "Lasha", "Lazarus", "Lea", "Leah", "Lebanon", "Lebaoth", 
                      "Lebbeus", "Lebonah", "Lecah", "Lehabim", "Lehem", "lehi", "Lekah", "Lemuel", "Leor", "Leshem", "Letushim", "Leummim", "Levi", "Leviticus", "libnah", "Libni", 
                      "Libya", "Likhi", "Lilith", "Linus,", "Lior", "Lo", "Lod", "Lois", "Lot", "Lubin", "Lucas", "Lucifer", "Lud", "Luhith", "Lukas", "Luke", "Luz", "Lycaonia", 
                      "Lydda", "Lydia", "Lysanias", "Lysias", "Lysimachus", "Lystra", "Laadah", "Laadan", "Macedonia", "Machbenah", "Machi", "Machir", "Machnadebai", "Machpelah", 
                      "Madai", "Madian", "Madmannah", "Madon", "Magbish", "Magdala", "Magdalene", "Magdiel", "Magog", "Magpiash", "Mahalah", "Mahaleleel", "Mahali", "Mahanaim", 
                      "Mahanehdan", "Maharai", "Mahath", "Mahavites", "Mahazioth", "Maher", "Mahlah", "Maim", "Makaz", "Makheloth", "Makkedah", "Malachi", "Malcham", "Malchiel", 
                      "Malchijah", "Malchus", "Maleleel", "Mallothi", "Malluch", "Mammon", "Mamre", "Manaen", "Manahethites", "Manasseh", "Manoah", "Maon", "Mara", "Marah", 
                      "Maralah", "Maranatha", "Marcaboth", "Marcus", "Mareshah", "Maria", "Mariah", "Mark", "Markus", "Maroth", "Marsena", "Martha", "Mary", "Mash", "Mashal", 
                      "Masrekah", "Massa", "Massah", "Mathias", "Matias", "Matred", "Matri", "Mattan", "Mattana", "Mattaniah,", "Mattatha", "Mattathias", "Mattenai", "Matthan", 
                      "Matthew", "Matthias", "Matthæus", "Mattias", "Mazzaroth", "Meah", "Mearah", "Mebunnai", "Medad", "Medan", "Medeba", "Media", "Megiddo", "Megiddon", 
                      "Mehetabel", "Mehida", "Mehir", "Meholah", "Mehujael", "Mehuman", "Mejarkon", "Mekonah", "melah", "Melatiah", "Melchi", "Melchiah", "Melchi-shua", 
                      "Melchizedek", "Melea", "Melech", "Melita", "Melzar", "Memphis", "Memucan", "Menahem", "Menan", "Mene", "Meon", "Meonenim", "Mephibosheth", "Mephaath", 
                      "Merab", "Meraioth", "Merari", "Mered", "Meremoth", "Meres", "Meribah", "Meribaal", "Merodach", "Merom", "Meronothite", "Meroz", "Mesha", "Meshach", 
                      "Meshech", "Meshelemiah", "Meshillemoth", "Mesobaite", "Mesopotamia", "Messiah", "Metheg", "Methusael", "Methuselah", "Meunim", "Mezahab", "Miamin", "Mibhar", 
                      "Mibsam", "Mibzar", "Micah", "Micaiah", "Micha", "Michael", "Michaiah", "Michal", "Michmash", "Michmethah", "Michri", "Michtam", "Middin", "Midian", "Migdal", 
                      "Migdol", "Migron", "Mijamin", "Mikael", "Mikloth", "Milalai", "Milcah", "Milcom", "Miletus", "Millo", "Miniamin", "Minni", "Minnith", "Miriam", "Mishael", 
                      "Mishal", "Misham", "Mishma", "Mishmannah", "Mishpat", "Mishraites", "Mispar", "Misrephoth", "Misti", "Mithcah", "Mithnite", "Mithredath", "Mitylene", "Mizar",
                      "Mizpah", "mizpeh", "Mizraim", "Mizzah", "Mnason", "Moab", "Moladah", "Molech", "Molid", "Mordecai", "Moreh", "Moriah", "Mosera", "Moserah", "Moseroth", "Moses",
                      "Mozah", "Muppim", "Mushi", "Myra", "Mysia", "Maachah", "Maachathi", "Maadai", "Maadiah", "Maai", "Maale", "Maarath", "Maaseiah", "Maasiai", "Maath", "Maaz", 
                      "Nabal", "Naboth", "Nachon", "Nachor", "Nadab", "Nagge", "Nahaliel", "Nahallal", "Naham", "Naharai", "Nahash", "Nahath", "Nahbi", "Nahor", "Nahshon", "Nahum", 
                      "Nain", "Naioth", "Naomi", "Naphish", "Naphtali", "Narcissus", "Nathan", "Nathanael", "Naum", "Nazareth", "Nazarite", "Neah", "Neapolis", "Neariah", "Nebai", 
                      "Nebaioth", "Neballat", "Nebat", "Nebo", "Nebuchadnezzar", "Nebuzaradan", "Necho", "Nedabiah", "Nehelamite", "Nehemiah", "Nehum", "Nehushta", "Nehushtan", 
                      "Neiel", "Nekoda", "Nemuel", "Nepheg", "Nephilim", "Nephish", "Nephishesim", "Nephthalim", "Nephtoah", "Nephusim", "Ner", "Nereus", "Nergal", "Neri", "Neriah", 
                      "Nethaneel", "Nethaniah", "Nethinim", "Neziah", "Nezib", "Nibhaz", "Nibshan", "Nicanor", "Nicodemus", "Nicolaitanes", "Nicolas", "Nicopolis", "Niger", "Nimrah",
                      "Nimrod", "Nimshi", "Nineveh", "Nisan", "Nisroch", "Nissi", "No", "Noa", "Noadiah", "Noah", "Nob", "Nobah", "Nod", "Nodab", "Noe", "Nogah", "Noha", "Non", 
                      "Noph", "Nophah", "Norah", "Nun", "Nymphas", "Naam", "Naamah", "Naaman", "Naarah", "Naaran", "Naashon", "Naasson", "Obadiah", "Obal", "Obed", "Obil", "Oboth",
                      "Ocran", "Oded", "Og", "Ohad", "Ohel", "Olympas", "Omar", "Omega", "Omri", "On", "Onan", "Onesimus", "Onesiphorus", "Ono", "Ophel", "Ophir", "Ophni", "Ophrah", 
                      "Oreb", "Oren", "Ornan", "Orpah", "Oshea", "Othni", "Othniel", "Ozem", "Ozias", "Ozni", "Padan", "Padon", "Pagiel", "Pahath", "Pai", "Palestina", "Palet", 
                      "Pallu", "Palti", "Pamphylia", "Paola", "Paphos", "Parah", "Paran", "Parbar", "Parmashta", "Parmenas", "Parnach", "Parosh", "Parshandatha", "Paruah", "Pas", 
                      "Pasach", "Paseah", "Pashur", "Patara", "Pathros", "Patmos", "Patrobas", "Pau", "Paul", "Paulus", "Pazzez", "Pedahzur", "Pedaiah", "Pekah", "Pekahiah", "Pekod", 
                      "Pelaiah", "Pelaliah", "Pelatiah", "Peleg", "Pelethites", "Pelonite", "Peniel", "Peninnah", "Pentapolis", "Pentateuch", "Pentecost", "Penuel", "Peor", "Perazim", 
                      "Peresh", "Perez", "Perga", "Pergamos", "Perida", "Perizzites", "Persia", "Persis", "Peruda", "Peter", "Pethahiah", "Pethuel", "Peulthai", "Phalec", "Phallu", 
                      "Phalti", "Phaltiel", "Phanuel", "Pharaoh", "Pharez", "Pharisees", "Pharpar", "Phebe", "Phelet", "Phenice", "Phichol", "Philadelphia", "Philemon", "Philetus", 
                      "Philip", "Philippi", "Philistines", "Philologus", "Phinehas", "Phlegon", "Phrygia", "Phurah", "Phygellus", "Phylacteries", "Pi", "Pilate", "Pileser", "Pinon", 
                      "Piram", "Pirathon", "Pisgah", "Pishon", "Pisidia", "Pison", "Pithom", "Pithon", "Pochereth", "Pontius", "Pontus", "Poratha", "Potiphar", "Potipherah", "Prisca", 
                      "Priscilla", "Prochorus", "Puah", "Publius", "Pudens", "Pul", "Punites", "Punon", "Puteoli", "Putiel", "paaneah", "Quartus", "Quirinius", "Rab", "Rabbah", "Rabbi",
                      "Rabbith", "Rabboni", "Rabmag", "Rabshakeh", "Raca", "Racal", "Rachab", "Rachal", "Rachel", "Raddai", "Ragau", "Raguel", "Rahab", "Raham", "Rakal", "Rakel", 
                      "Rakem", "Rakkath", "Rakkon", "Ram", "Ramah", "Ramath", "Ramathaim", "Ramiah", "Ramoth", "rapha", "Raphah", "Raphu", "Reaiah", "Reba", "Rebecca", "Rebekah", 
                      "Rebekka", "Rechab", "Reelaiah", "Regem", "Regemmelech", "Rehabiah", "Rehob", "Rehoboam", "Rehoboth", "Rehum", "Rei", "Rekem", "Remaliah", "Remmon", "Remphan", 
                      "Rephael", "Rephaiah", "Rephaim", "Rephidim", "Resen", "Reu", "Reuben", "Reuel", "Reumah", "Rezeph", "Rezin", "Rezon", "Rhegium", "Rhesa", "Rhoda", "Rhodes", 
                      "Ribai", "Riblah", "Rimmon", "Rinnah", "Riphath", "rishathaim", "Rissah", "Rithmah", "Rizpah", "Rogel", "Rogelim", "Rohgah", "Roi", "Romamti", "Roman", "Rome", 
                      "Rosh", "Rufus", "Ruhamah", "Rumah", "Ruth", "Raamah", "Raamiah", "Sabaeans", "Sabaoth", "Sabtah", "Sabtechah", "Sacar", "Sachia", "Sadducees", "Sadoc", 
                      "Sahadutha", "Sakia", "Salah", "Salamis", "Salathiel", "Salcah", "Salem", "Salim", "Sallai", "Salma", "Salmon", "Salome", "Samaria", "Samlah", "Samos", 
                      "Samothracia", "Samson", "Samuel", "Sanballat", "Sanhedrin", "Sannah", "Sansannah", "Saph", "Saphir", "Sapphira", "Sara", "Sarah", "Sarai", "Sardis", "Sardites", 
                      "Sarepta", "Sargon", "Sarid", "saris", "Saron", "Sarsechim", "Saruch", "Satan", "Saul", "Sceva", "Seba", "Sebat", "Sebia", "Secacah", "Sechu", "Secundus", "Segub",
                      "Seir", "Sela", "Selah", "Seled", "Seleucia", "Sem", "Semachiah", "Semei", "Seneh", "Senir", "Sennacherib", "Senaah", "Seorim", "Sephar", "Sepharad", "Sepharvaim",
                      "Sepher", "Serah", "Seraiah", "Seraphim", "Sered", "Sergius", "Serug", "Seth", "Sethur", "Shabbethai", "Shachia", "Shadrach", "Shage", "shahar", "Shalal", "Shalem", 
                      "Shalim", "Shalisha", "Shallum", "Shalmai", "Shalman", "Shalmaneser", "Shalom", "Shamariah", "Shamayim", "Shamer", "Shamgar", "Shamhuth", "Shamir", "Shammah", 
                      "Shammai", "Shammoth", "Shammua", "Shamsherai", "Shapham", "Shaphat", "Sharai", "Sharar", "Sharezer", "Sharon", "Shashai", "Shashak", "Shaul", "Shaveh", "Shealtiel", 
                      "Shear", "Sheariah", "Sheba", "Shebam", "Shebaniah", "Shebarim", "Sheber", "Shebna", "Shebuel", "Shecaniah", "Shechem", "Shedeur", "Shehariah", "Shelah", "Shelemiah", 
                      "Sheleph", "Shelesh", "Shelomi", "Shelumiel", "Shem", "Shema", "Shemaiah", "Shemariah", "Shemeber", "Shemed", "Shemer", "Shemesh", "Shemida", "Sheminith", "Shemiramoth", 
                      "Shemuel", "Shen", "Shenazar", "Shenir", "Shephatiah", "Shephi", "Shepho", "Shephuphan", "sherah", "Sherebiah", "Sheshach", "Sheshai", "Sheshan", "Sheshbazzar", 
                      "Shethar", "Sheva", "Shibboleth", "Shibmah", "Shicron", "Shiggaion", "Shihon", "Shihor", "Shilhi", "Shillem", "Shilo", "Shiloah", "Shiloh", "Shilom", "Shilshah", 
                      "Shimeah", "Shimei", "Shimeon", "Shimma", "Shimon", "Shimrath", "Shimri", "Shimrith", "Shimshai", "Shinab", "Shinar", "Shiphi", "Shiphrah", "Shisha", "Shishak", 
                      "Shitrai", "Shittim", "Shiza", "Shoa", "Shobab", "Shobach", "Shobai", "Shobal", "Shobek", "Shochoh", "Shoham", "Shomer", "Shophach", "Shophan", "Shoshannim", "Shua", 
                      "Shuah", "Shual", "Shubael", "Shuham", "Shulamite", "Shunem", "Shuni", "Shuphim", "Shur", "Shushan", "Shuthelah", "Shaalabbin", "Shaalbim", "Shaalbonites", "Shaaph", 
                      "Shaaraim", "Shaashgaz", "Sia", "Sibbechai", "Sibmah", "Sichem", "Siddim", "Sidon", "Sigionoth", "Sihon", "Sihor", "Silas", "Silla", "Siloa", "Silvanus", "Simeon", 
                      "Simon", "Sin", "Sinai", "Sinim", "Sinon", "Sion", "Sippai", "Sirach", "Sisamai", "Sisera", "Sitnah", "Sivan", "Smyrna", "So", "Socoh", "Sodi", "Sodom", "Solomon", 
                      "Sopater", "Sophereth", "Sorek", "Sosthenes", "Sotai", "Spain", "Stachys", "Stephanas", "Stephen", "Suah", "Succoth", "Sud", "Sur", "Susah", "Susanna", "Susi", "Susim", 
                      "Sychar", "Syene", "Syntyche", "Syracuse", "Tabbaoth", "Tabbath", "Tabeal", "Tabelel", "Taberah", "Tabering", "Tabitha", "Tabor", "Tabrimon", "Tadmor", "Tahan", 
                      "Tahapenes", "Tahath", "Tahpenes", "Tahrea", "Talitha", "Talmai", "Tamah", "Tamar", "Tammuz", "Tanhumeth", "Taphath", "Tappuah", "Tarah", "Taralah", "Tarea", 
                      "Tarpelites", "Tarshish", "Tarsus", "Tartak", "Tartan", "Tatnai", "Tebah", "Tebaliah", "Tebeth", "Tehinnah", "Tekel", "Tekoa", "Tel", "Telabib", "Telah", "Telassar", 
                      "Telem", "Telharsa", "Tema", "Teman", "Terah", "Teraphim", "Tertius", "Tertullus", "Tetrarch", "Thaddeus", "Thahash", "Thamah", "Thamar", "Tharah", "Thebez", "Thelasar", 
                      "Theophilus", "Thessalonica", "Theudas", "Thomas", "Thuhash", "Thummim", "Thyatira", "Tibbath", "Tiberias", "Tiberius", "Tibni", "Tidal", "Tiglath", "Tikvah", 
                      "Tilon", "Timeus", "Timnah", "Timnath", "Timon", "Timotheus", "Timothy", "Tiphsah", "Tire", "Tirhakah", "Tiria", "Tirras", "Tirshatha", "Tirza", "Tirzah", "Tishbite", 
                      "Titus", "Toah", "Tob", "Tobiah", "Toby", "Tochen", "Togarmah", "Tohu", "Toi", "Tola", "Tophet", "Topheth", "Trachonitis", "Troas", "Trophimus", "Tryphena", "Tryphon", 
                      "Tryphosa", "Tsidkenu", "Tubal", "Tychicus", "Tyrannus", "Tyrus", "Taanach", "Ucal", "Uel", "Ulai", "Ulam", "Ulla", "Ummah", "Unni", "Upharsin", "Uphaz", "Ur", "Urbane", 
                      "Uri", "Uriah", "Uriel", "Urim", "Uthai", "Uz", "Uzai", "Uzal", "Uzza", "Uzzah", "Uzzen", "Uzzi", "Uzziah", "Vajezatha", "Vaniah", "Vashni", "Vashti", "Vophsi", "Yacob", 
                      "Yahweh", "Yakob", "Yasaf", "Yehezkel", "Yehoyada", "Yehu", "Yeshua", "Yoab", "Yonah", "Yuval", "Zabad", "Zabbai", "Zabbud", "Zabdi", "Zabdiel", "Zaccai", "Zacchaeus", 
                      "Zaccur", "Zachariah", "Zacharias", "Zacher", "Zadok", "Zaham", "Zair", "Zalaph", "Zalmon", "Zalmonah", "Zalmunna", "Zamzummims", "Zanoah", "Zaphnath", "Zarah", 
                      "Zareathites", "Zared", "Zarephath", "Zaretan", "Zareth", "Zarhites", "Zartanah", "Zarthan", "Zatthu", "Zattu", "Zavan", "Zaza", "Zebadiah", "Zebah", "Zebaim", "Zebedee", 
                      "Zebina", "Zeboiim", "Zeboim", "Zebub", "Zebudah", "Zebul", "Zebulonite", "Zebulun", "Zebulunites", "Zechariah", "Zedad", "Zedek", "Zedekiah", "Zeeb", "Zelah", "Zelek", 
                      "Zelophehad", "Zelotes", "Zelzah", "Zemaraim", "Zemarite", "Zemira", "Zenan", "Zenas", "Zephaniah", "Zephath", "Zephathah", "Zephi", "Zepho", "Zephon", "Zephonites", 
                      "Zer", "Zerah", "Zerahiah", "Zered", "Zereda", "Zeredathah", "Zererath", "Zeresh", "Zereth", "Zeri", "Zeror", "Zeruah", "Zerubbabel", "Zeruiah", "Zetham", "Zethan", 
                      "Zethar", "Zia", "Ziba", "Zibeon", "Zibia", "Zibiah", "Zichri", "Ziddim", "Zidkijah", "Zidon", "Zidonians", "Zif", "Ziha", "Ziklag", "Zillah", "Zilpah", "Zilthai", 
                      "Zimmah", "Zimran", "Zimri", "Zin", "Zina", "Zion", "Zior", "Ziph", "Ziphah", "Ziphims", "Ziphion", "Ziphites", "Ziphron", "Zippor", "Zipporah", "Zithri", "Ziz", "Ziza", 
                      "Zizah", "Zoan", "Zoar", "Zoba", "Zobah", "Zobebah", "Zohar", "Zoheleth", "Zoheth", "Zophah", "Zophai", "Zophar", "Zophim", "Zorah", "Zorathites", "Zoreah", "Zorites", 
                      "Zorobabel", "Zuar", "Zuph", "Zur", "Zuriel", "Zurishaddai", "Zuzim", "Zaanaim", "Zaanannim", "Zaavan", "Æneas", "Ænon", "Aaron" };

                    var bible = new Category() {
                        Title = "Bibelen",
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

                    var starTrekList = new List<string>() { "Adami", "Airiam", "Alynna", "Alyssa", "Amanda", "Antos", "Archer", "Ash", "Asha", "Ayala", "Azan", "Barclay", 
                    "Bareil", "Bashir", "Batten", "Beckett", "Beverly", "Boimler", "Boothby", "Boyce", "Brad", "Brien", "Brunt", "Bryce", "Burnham", "Carey", "Carol", "Celes", 
                    "Chakotay", "Chapel", "Charles", "Chekov", "Chell", "Christopher", "Colt", "Cornwell", "Cretak", "Cristobal", "Crusher", "Culber", "Culluh", "Cutler", 
                    "Cyia", "Damar", "Daniels", "Data", "Dax", "Deanna", "Degra", "Detmer", "Dolim", "Dukat", "Eddington", "Edon", "Ehleyr", "Elanna", "Elim Garak", "Elnor", 
                    "Enabran", "Erika", "Evek", "Ezri", "Fontaine", "Forge", "Forrest", "Gabriel", "Gabrielle", "Garrison", "Gen", "Geordi", "George", "Georgiou", "Gomez", 
                    "Gowron", "Grayson", "Guinan", "Hayes", "Hernandez", "Hikaru", "Hogan", "Homn", "Hoshi", "Hugh", "Icheb", "Ishka", "Jadzia", "Jal", "James", "Janeway", 
                    "Janice", "Jannar", "Jean", "Jennifer", "Jet", "Joann", "Jonathan", "José", "Joseph", "Julian", "Jurati", "Kashimuro", "Kasidy", "Katherine", "Kathryn", 
                    "Katrina", "Keiko", "Kes", "Keyla", "Khan", "Kim", "Kimara", "Kira", "Kirk", "Kol", "Kor", "Kurn", "La", "Laren", "Laris", "Leeta", "Lefler", "Leland", 
                    "Leonard", "Li Nalas", "Linus", "Lon", "Lorca", "Lore", "Luc", "Lursa", "Lwaxana", "Maihar", "Malcolm", "Mallora", "Marcus", "Mariner", "Martok", "Maxwell", 
                    "Mayweather", "McCoy", "Mezoti", "Mila", "Miles", "Molly", "Montgomery", "Mora", "Morn", "Mot", "Mudd", "Musiker", "Naomi", "Narek", "Narissa", "Nechayev", 
                    "Neelix", "Nerys", "Nhan", "Nicoletti", "Nilsson", "Nog", "Noonien", "Nozawa", "Nyota", "Odo", "Ogawa", "Oh", "Opaka", "Owen", "Owosekun", "Pavel", 
                    "Philippa", "Phillip", "Phlox", "Picard", "Pike", "Pol", "Pollard", "Pulaski", "Quark", "Raffi", "Rand", "Rebi", "Reed", "Reginald", "Rell", "Renee", "Reno", 
                    "Rhys", "Riker", "Rios", "Rizzo", "Ro", "Robin", "Ross", "Rostov", "Rozhenko", "Saavik", "Samantha", "Sarek", "Saru", "Sato", "Scott", "Sela", "Seska", 
                    "Shakaar", "Shran", "Silik", "Singh", "Sisko", "Sloan", "Soji", "Sonya", "Soval", "Spock", "Stamets", "Suder", "Sulan", "Sulu", "Susan", "Sylvia", "Tain", 
                    "Tal", "Tasha", "Tilly", "Tomalak", "Tora", "Torres", "Tracy", "Travis", "Troi", "Tucker", "Tuvok", "Tyler", "Uhura", "Una", "Vash", "Vic", "Voq", "Vorik", 
                    "Wesley", "Weyoun", "Wildman", "William", "Winn", "Worf", "Yar", "Yates", "Zek", "Zhaban", "Ziyal" };

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
                    "Finrod", "Finwë", "Frodo", "Galadriel", "Gamgee", "Gandalf", "Gil-galad", "Gimli", "Glorfindel", "Goldberry", "Gollum", "Gríma", "Húrin", "Idril", "Indis",
                    "Isildur", "Kíli", "Legolass", "Lúthien", "Maedhros", "Melian", "Melkor", "Meriadoc", "Merry", "Míriel", "Morgoth", "Okensheild", "Peregrin", "Pippin", 
                    "Radagast", "Sam", "Samwise", "Saruman", "Sauron", "Shelob", "Smaug", "Sméagol", "Théoden", "Thingol", "Thorin", "Thranduil", "Tom", "Took", "Treebeard", 
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
                        Title = "Falloscentrisk",
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

                    var emotionsList = new List<string>() { "Håb", "Triumf", "Sejr", "Hope", "Joy", "Victory" };

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
                    "Charles Mason", "Jeremiah Dixon", "Thomas Edison", "Boy George", "David Bowie", "Elvis Presley", "Bob Paisley", "Miley Cyrus", "Stevie Wonder", 
                    "Michael Owen", "Salman Rushdi", "Sofie Bonde", "Von Trier", "Kong Ramses", "King Solomon", "Papa Bue", "Mama Cass", "Steven Gerrard", "Smokey Robinson", 
                    "Hugh Grant", "Prince John", "King Richard", "Queen Mary", "Queen Elizabeth", "Prince Charles", "Prince William", "Princess Diana", "Prince Harry", 
                    "Bonny Tyler", "Lewis Carol", "Don Johnson", "Tom Cruise", "Benjamin Franklin", "Demi More", "Michael Douglas", "Bonnie Prince Charles", "Paris Hilton",
                    "Bryan Adams", "Rod Stewart", "Hans Kirk", "Bille August", "Jack London", "John Milton", "Tom Hanks", "James Barrie", "Robert Scott", "Harper Lee", 
                    "Herman Melville", "Antonio Vivaldi", "Dina Jewel", "Peter North", "Ron Jeremy", "Evan Stone", "Michael Keaton", "Michael Fox", "Bob Marley", "Ruby Dee",
                    "Jim Davis", "Viven Leigh", "Samantha Bee", "Margaret Cho", "Elizabeth Taylor", "Lucy Liu", "Agatha Christie", "George Eliot", "Marie Curie", "Carol King",
                    "Billie King", "Brune Mars", "Bent Fabricius Bjerre", "Nat King Cole", "Donna Summer", "Hanne Boel", "Anne Linett", "Cliff Richard", "Keith Richard", 
                    "Mick Jagger", "Peter Andre", "Eddy Grant", "Billy Idol", "Bon Jovi", "Isaac Newton", "Kong Christian", "Kong Hans", "Kong Frederik" };
                
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

