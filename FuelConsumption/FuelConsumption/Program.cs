using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;

namespace FuelConsumption
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var users = new Dictionary<int, List<object>>
            {
                {575,
                    new List<object>
                    { "Dina",
                      "Iladan",
                      new DateTime(2014,02,20),
                      new List<(string, DateTime, double, double, double, double)>
                      { ("Bih", new DateTime(2020,03,30), 300, 40, 1.82, 10*1.82),
                        ("Cro", new DateTime(2021,04,20), 100, 10, 1.82, 40*1.82),
                        ("Ger", new DateTime(2020,03,30), 150, 30, 1.52, 30*1.52)
                      }
                    }
                },
                {555,
                    new List<object>
                    { "Ivan",
                      "Gvcevic",
                      new DateTime(2004, 01, 10),
                      new List<(string, DateTime, double, double, double, double)>
                      { ("Slo", new DateTime(2020,02,20), 200, 20, 1.82, 20*1.82)
                      }
                    }
                }
            };
            while (true)
            {
                Console.WriteLine("1 - Korisnici");
                Console.WriteLine("2 - Putovanja");
                Console.WriteLine("0 - Izlaz iz aplikacije");
                var firstChoice = -1;
                do
                {
                    Console.WriteLine("Unesite zeljenu opciju (0,1,2): ");
                    if (int.TryParse(Console.ReadLine(), out firstChoice))
                    {
                        Console.WriteLine("Unijeli ste broj {0}", firstChoice); 
                    }
                    else {
                        firstChoice = -1;// ne shvacam zasto mi inace postavi na 0, ako upisem slovo
                        Console.WriteLine("Potrebno je unijeti broj");
                    }
                    

                } while (firstChoice != 0 && firstChoice != 1 && firstChoice != 2);

                switch (firstChoice)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        Users_function(users);
                        break;
                    case 2:
                        Travels(users);
                        break;
                }
            }


        }

        static void Users_function( Dictionary<int,List<object>> users) {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Korisnici: ");
                Console.WriteLine("1 - Unos novog korisnika \r\n2 - Brisanje korisnika \r\n3 - Uređivanje korisnika \r\n4 - Pregled svih korisnika \r\n0 - Povratak na glavni izbornik");
                int user_choice = -1;

                while (user_choice != 0 && user_choice != 1 && user_choice != 2 && user_choice != 3 && user_choice != 4)
                {
                    Console.Write("Unesite zeljeni odabir: ");
                    if (int.TryParse(Console.ReadLine(), out user_choice))
                    {
                        Console.WriteLine("Potrebno je unijeti jedan od ponudjenih brojeva");
                    }
                    else Console.WriteLine("Potrebno je unijeti broj! ");
                }

                switch (user_choice)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        Console.WriteLine("Dodavanje novog korisnika: ");
                        int new_user_id;
                        while (true)
                        {
                            Console.Write("Unesite id korisnika ");
                            if (int.TryParse(Console.ReadLine(), out new_user_id))
                            {
                                break;
                            }
                            else { Console.WriteLine("Trebate unijeti broj za id "); }

                        }

                        Console.Write("Unesite ime korisnika "); string new_user_name = Console.ReadLine();
                        new_user_name = new_user_name ?? "User name";
                        Console.Write("Unesite prezime korisnika "); string new_user_surname = Console.ReadLine();
                        new_user_surname = new_user_surname ?? "User surname";

                        DateTime new_user_birth_date;
                        while (true)
                        {
                            Console.Write("Unesite datum rodjenja korisnika (YYYY-MM-DD) ");
                            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out new_user_birth_date))
                            { //mala slova za godinu i dane!!
                                break;
                            }
                            else { Console.WriteLine("Niste unijeli datum u ispravnom formatu "); }

                        }

                        Console.WriteLine("Ako zelite dodat putovanja vratite se na glavni izbornik.");
                        var new_user_travels = new List<(string, DateTime, double, double, double, double)>();

                        users.Add(new_user_id, new List<object> { new_user_name, new_user_surname, new_user_birth_date, new_user_travels });
                        break;

                    case 2:

                        var deleteChoice = 0;
                        while (deleteChoice != 1 && deleteChoice != 2)
                        {
                            Console.WriteLine("Zelite li obrisati korisnika: \n 1 - po ID-u\n 2 - po imenu ");
                            if (int.TryParse(Console.ReadLine(), out deleteChoice))
                            {
                                Console.WriteLine("Vas odabir je {0}", deleteChoice);
                            }
                            else Console.WriteLine("Treba bit broj");
                        }

                        if (deleteChoice == 1)
                        {
                            int deleteByID = 0;
                            while (true)
                            {
                                Console.WriteLine("Unesite id korisnika kojeg zelite izbrisat ");
                                if (int.TryParse(Console.ReadLine(), out deleteByID))
                                {
                                    if (users.ContainsKey(deleteByID))
                                    {
                                        users.Remove(deleteByID);
                                        Console.WriteLine("Izbrisali smo korisnika s IDiem {0}", deleteByID);
                                        break;
                                    }
                                    else Console.WriteLine("Korisnik s unesenim ID-iem ne postji");
                                }
                                else Console.WriteLine("Treba bit broj");
                            }
                        }
                        else
                        {
                            string deleteByName = "";

                            while (true)
                            {
                                var keyForDelete = 0;
                                Console.Write("Unesite ime korisnika kojeg zelite izbrisat ");
                                deleteByName = Console.ReadLine();

                                if (string.IsNullOrEmpty(deleteByName))
                                {
                                    Console.WriteLine("Ne mozete ostavit prazno");
                                    continue;
                                }

                                foreach (var id in users.Keys)
                                {
                                    if (users[id][0].ToString().Equals(deleteByName, StringComparison.OrdinalIgnoreCase))
                                    //potrebno ako je razlika u malim i velikim slovima
                                    {
                                        keyForDelete = id;
                                        break;
                                    }

                                }
                                if (keyForDelete != 0)
                                {
                                    users.Remove(keyForDelete);
                                    Console.WriteLine("Izbrisali smo korisnika s imenom {0}", deleteByName);
                                    break;
                                }
                                else Console.WriteLine("Ne postoji korisnik s unesenim imenom!");
                            }

                        }

                        break;
                    case 3:
                        int edit_user = 0;
                        while (true)
                        {
                            Console.WriteLine("Unesite id korisnika kojeg zelite urediti ");
                            if (int.TryParse(Console.ReadLine(), out edit_user))
                            {
                                if (users.ContainsKey(edit_user))
                                {
                                    Console.Write("Unesite novo ime korisnika "); string edit_name = Console.ReadLine();
                                    edit_name = edit_name ?? "User name";
                                    Console.Write("Unesite prezime korisnika "); string edit_surname = Console.ReadLine();
                                    edit_surname = edit_surname ?? "User surname";

                                    DateTime edit_birth_date;
                                    while (true)
                                    {
                                        Console.Write("Unesite datum rodjenja korisnika (YYYY-MM-DD) ");
                                        if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out edit_birth_date))
                                        {
                                            break;
                                        }
                                        else { Console.WriteLine("Niste unijeli datum u ispravnom formatu "); }

                                    }
                                    var old_travels = (List<string>)users[edit_user][3]; //treba castat jer u suprotnom vraca objekt
                                    users[edit_user] = new List<object> { edit_name, edit_surname, edit_birth_date, old_travels };
                                    Console.WriteLine("Uredili smo korisnika s IDiem {0}", edit_user);
                                    break;
                                }
                                else Console.WriteLine("Korisnik s unesenim ID-iem ne postji");
                            }
                            else Console.WriteLine("Treba bit broj");
                        }
                        break;
                    case 4:

                        var viewChoice = 0;
                        while (viewChoice != 1 && viewChoice != 2 && viewChoice != 3)
                        {
                            Console.WriteLine("Odaberite zeljeni pregled korisnika:\n1 - ispis svih korisnika abecedno po prezimenu\n2 - svih onih koji imaju više od 20 godina\n3 - svih onih koji imaju barem 2 putovanja");
                            if (int.TryParse(Console.ReadLine(), out viewChoice))
                            {
                                Console.WriteLine("Vas odabir je {0}", viewChoice);
                            }
                            else Console.WriteLine("Treba bit broj");
                        }

                        if (viewChoice == 1)
                        {
                            Console.WriteLine("Korisnici sortirani abecedno po prezimenima ");
                            var list_of_surnames = new List<string>();
                            foreach (int user_id in users.Keys)
                            {
                                string surname = users[user_id][1].ToString();
                                list_of_surnames.Add(surname);
                            }
                            list_of_surnames.Sort();

                            foreach (var surname in list_of_surnames)
                            {
                                foreach (var id in users.Keys)
                                {
                                    if (users[id][1].ToString().Equals(surname, StringComparison.OrdinalIgnoreCase))
                                    {
                                        Console.WriteLine($"{id} - {users[id][0]} - {users[id][1]} - {((DateTime)users[id][2]).ToShortDateString()}"); //ne zelimo sate,min,sek
                                    }
                                }

                            }

                        }
                        else if (viewChoice == 2)
                        {
                            foreach (var key_value_pair in users)
                            {
                                var id = key_value_pair.Key;
                                var name = key_value_pair.Value[0].ToString();
                                var surname = key_value_pair.Value[1].ToString();
                                var birthdate = (DateTime)key_value_pair.Value[2];
                                int age = DateTime.Now.Year - birthdate.Year;
                                if (age > 20)
                                {
                                    Console.WriteLine($"{id} - {name} - {surname} - {((DateTime)birthdate).ToShortDateString()}");
                                }
                            }
                        }
                        else if (viewChoice == 3)
                        {
                            foreach (var key_value_pair in users)
                            {
                                var id = key_value_pair.Key;
                                var name = key_value_pair.Value[0].ToString();
                                var surname = key_value_pair.Value[1].ToString();
                                var birthdate = (DateTime)key_value_pair.Value[2];
                                var travels = (List<string>)key_value_pair.Value[3];

                                if (travels.Count >= 2)
                                {
                                    Console.WriteLine($"{id} - {name} - {surname} - {((DateTime)birthdate).ToShortDateString()}");
                                }
                            }
                        }
                        break;
                }
            }
            
        }
        static void Travels(Dictionary<int, List<object>> users)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Putovanja: ");
                Console.WriteLine("1 - Unos novog putovanja \r\n2 - Brisanje putovanja \r\n3 - Uređivanje postojećeg putovanja \r\n4 - Pregled svih putovanja \r\n5 - Izvještaji i analize \r\n0 - Povratak na glavni izbornik");
                int travel_choice = -1;

                while (travel_choice != 0 && travel_choice != 1 && travel_choice != 2 && travel_choice != 3 && travel_choice != 4 && travel_choice != 5)
                {
                    Console.Write("Unesite zeljeni odabir: ");
                    if (int.TryParse(Console.ReadLine(), out travel_choice))
                    {
                        Console.WriteLine("Potrebno je unijeti jedan od ponudjenih brojeva");
                    }
                    else Console.WriteLine("Potrebno je unijeti broj! ");
                }

                switch (travel_choice)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        while (true)
                        {
                            Console.WriteLine("Unesite id korisnika kojem zelite dodat putovanje ");
                            if (int.TryParse(Console.ReadLine(), out var userID))
                            {
                                if (users.ContainsKey(userID))
                                {
                                    Console.WriteLine("Unesite naziv putovanja "); var new_travel_name = Console.ReadLine();
                                    new_travel_name = new_travel_name ?? "Putovanje";
                                    
                                    DateTime new_travel_date;
                                    while (true)
                                    {
                                        Console.Write("Unesite datum putovanja (YYYY-MM-DD) ");
                                        if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out new_travel_date))
                                        {
                                            break;
                                        }
                                        else { Console.WriteLine("Niste unijeli datum u ispravnom formatu "); }

                                    }
                                    double new_distance = 0;
                                    while (true) {
                                        Console.WriteLine("Unesite prijedjenu kilometrazu: ");
                                        if (double.TryParse(Console.ReadLine(), out new_distance) && new_distance > 0)
                                        {
                                            break;
                                        }
                                        else Console.WriteLine("Potrebno je unijeti broj ");
                                    }
                                    double new_spent_fuel = 0;
                                    while (true)
                                    {
                                        Console.WriteLine("Unesite iznos potrosenog goriva: ");
                                        if (double.TryParse(Console.ReadLine(), out new_spent_fuel) && new_spent_fuel > 0)
                                        {
                                            break;
                                        }
                                        else Console.WriteLine("Potrebno je unijeti broj ");
                                    }
                                    double new_fuel_price = 0;
                                    while (true)
                                    {
                                        Console.WriteLine("Unesite cijenu litre goriva: ");
                                        if (double.TryParse(Console.ReadLine(), out new_fuel_price) && new_fuel_price > 0)
                                        {
                                            break;
                                        }
                                        else Console.WriteLine("Potrebno je unijeti broj ");
                                    }
                                    var new_total = new_spent_fuel * new_fuel_price;
                                    var new_travel = (new_travel_name, new_travel_date, new_distance, new_spent_fuel, new_fuel_price, new_total);

                                    ((List<(string, DateTime, double, double, double, double)>)users[userID][3]).Add(new_travel);
                                    
                                    break;

                                }
                                else Console.WriteLine("Korisnik s unesenim ID-iem ne postji");
                            }
                            else Console.WriteLine("Treba bit broj");
                        }

                        break;
                    case 2:
                        var deleteChoice = 0;
                        while (deleteChoice != 1 && deleteChoice != 2 && deleteChoice != 3)
                        {
                            Console.WriteLine("Brisanje: \n 1 - po ID-u\n 2 - svih putovanja skupljih od unesenog iznosa\n 3 - svih putovanja jeftinijih od unesenog iznosa ");
                            if (int.TryParse(Console.ReadLine(), out deleteChoice))
                            {
                                Console.WriteLine("Vas odabir je {0}", deleteChoice);
                            }
                            else Console.WriteLine("Treba bit broj");
                        }

                        if (deleteChoice == 1)
                        {
                            int deleteByID = 0;
                            while (true)
                            {
                                Console.WriteLine("Unesite id korisnika kojemu zelite izbrisat putovanja");
                                if (int.TryParse(Console.ReadLine(), out deleteByID))
                                {
                                    if (users.ContainsKey(deleteByID))
                                    {
                                        ((List<(string, DateTime, double, double, double, double)>)users[deleteByID][3]).Clear();
                                        Console.WriteLine("Izbrisali smo sva putovanja korisniku s IDiem {0}", deleteByID);
                                        break;
                                    }
                                    else Console.WriteLine("Korisnik s unesenim ID-iem ne postji");
                                }
                                else Console.WriteLine("Treba bit broj");
                            }
                        }
                        
                        else if (deleteChoice == 2)
                        {
                            double largest_amount = 100;
                            while (true)
                            {
                                Console.WriteLine("Uneite iznos iznad kojeg ce se sva putovanja izbrisat");
                                if (double.TryParse(Console.ReadLine(), out largest_amount) && largest_amount > 0)
                                {
                                    foreach(var keyValuePair in users)
                                    {
                                        var all_travels = (List<(string, DateTime, double, double, double, double)>)keyValuePair.Value[3];

                                        int remove = all_travels.RemoveAll(travel => travel.Item6 > largest_amount);

                                        if (remove > 0)
                                        {
                                            Console.WriteLine("Korisniku s IDiem: {0} je obrisano {1} putovanja.", keyValuePair.Key, remove);
                                        }
                                    }
                                    break;
                                }
                                else Console.WriteLine("Treba bit broj");
                            }
                        }
                        else if (deleteChoice == 3)
                        {
                            double lowest_amount = 100;
                            while (true)
                            {
                                Console.WriteLine("Uneite iznos ispod kojeg ce se sva putovanja izbrisat");
                                if (double.TryParse(Console.ReadLine(), out lowest_amount) && lowest_amount > 0)
                                {
                                    foreach (var keyValuePair in users)
                                    {
                                        var all_travels = (List<(string, DateTime, double, double, double, double)>)keyValuePair.Value[3];

                                        int remove = all_travels.RemoveAll(travel => travel.Item6 < lowest_amount);

                                        if (remove > 0)
                                        {
                                            Console.WriteLine("Korisniku s IDiem: {0} je obrisano {1} putovanja.", keyValuePair.Key, remove);
                                        }
                                    }
                                    break;
                                }
                                else Console.WriteLine("Treba bit broj");
                            }
                        }
                        break;
                    case 3:
                        int edit_user_travels = 0;
                        while (true)
                        {
                            Console.WriteLine("Unesite id korisnika cija putovanja zelite urediti ");
                            if (int.TryParse(Console.ReadLine(), out edit_user_travels))
                            {
                                if (users.ContainsKey(edit_user_travels))
                                {
                                    var all_travels = (List<(string, DateTime, double, double, double, double)>)users[edit_user_travels][3];
                                    for (int i = 0; i < all_travels.Count; i++)
                                    {
                                        var travel = all_travels[i];
                                        Console.WriteLine("Zelite li promijeniti naziv putovanja, trenutno je {0}\n ako zelite upisite da", travel.Item1);
                                        if ((Console.ReadLine() ?? "").Trim().ToLower() == "da")
                                        {
                                            Console.WriteLine("Unesite novi naziv putovanja ");
                                            var edit_travel_name = Console.ReadLine();
                                            edit_travel_name = string.IsNullOrWhiteSpace(edit_travel_name) ? travel.Item1 : edit_travel_name;
                                            var updated_travel = (edit_travel_name, travel.Item2, travel.Item3, travel.Item4, travel.Item5, travel.Item6);
                                            all_travels[i] = updated_travel;
                                            Console.WriteLine("Promijenut je naziv putovanja u {0}", edit_travel_name);
                                        }

                                        Console.WriteLine("Zelite li promijeniti datum putovanja, trenutno je {0}\n ako zelite upisite da", travel.Item2);
                                        if ((Console.ReadLine() ?? "").Trim().ToLower() == "da")
                                        {
                                            DateTime edit_travel_date;
                                            while (true)
                                            {
                                                Console.Write("Unesite novi datum putovanja (YYYY-MM-DD) ");
                                                if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out edit_travel_date))
                                                {
                                                    var updated_travel = (travel.Item1, edit_travel_date, travel.Item3, travel.Item4, travel.Item5, travel.Item6);
                                                    all_travels[i] = updated_travel;
                                                    Console.WriteLine("Promijenut je datum putovanja u {0}", edit_travel_date);
                                                    break;
                                                }
                                                else  Console.WriteLine("Niste unijeli datum u ispravnom formatu ");
                                            }
                                        }
                                        Console.WriteLine("Zelite li promijeniti prjedjenu kilometrazu na putovanju, trenutno je {0}\n ako zelite upisite da", travel.Item3);
                                        if ((Console.ReadLine() ?? "").Trim().ToLower() == "da")
                                        {
                                            double edit_travel_distance = 0;
                                            while (true)
                                            {
                                                Console.WriteLine("Unesite novu kilometrazu putovanja ");
                                                if (double.TryParse(Console.ReadLine(), out edit_travel_distance) && edit_travel_distance > 0)
                                                {
                                                    var updated_travel = (travel.Item1, travel.Item2, edit_travel_distance, travel.Item4, travel.Item5, travel.Item6);
                                                    all_travels[i] = updated_travel;
                                                    break;
                                                }
                                                else Console.WriteLine("Potrebno je unijeti broj ");
                                            }
                                            Console.WriteLine("Promijenuta je kilometraza putovanja u {0}", edit_travel_distance);
                                        }
                                        Console.WriteLine("Zelite li promijeniti iznos potrosenog goriva na putovanju, trenutno je {0}\n ako zelite upisite da", travel.Item4);
                                        if ((Console.ReadLine() ?? "").Trim().ToLower() == "da")
                                        {
                                            double edit_spent_fuel = 0;
                                            double edit_total = 0;
                                            while (true)
                                            {
                                                Console.WriteLine("Unesite novi iznos potrosenog goriva na putovanju ");
                                                if (double.TryParse(Console.ReadLine(), out edit_spent_fuel) && edit_spent_fuel > 0)
                                                {
                                                    edit_total = edit_spent_fuel * travel.Item5;
                                                    var updated_travel = (travel.Item1, travel.Item2, travel.Item3, edit_spent_fuel, travel.Item5, edit_total);
                                                    all_travels[i] = updated_travel;
                                                    break;
                                                }
                                                else Console.WriteLine("Potrebno je unijeti broj ");
                                            }
                                            Console.WriteLine("Promijenut je iznos potrosenog goriva na putovanju u {0}\nS obzirom na njega promijenio se i total na {1}", edit_spent_fuel, edit_total);
                                        }
                                        Console.WriteLine("Zelite li promijeniti cijenu po litri potrosenog goriva na putovanju, trenutno je {0}\n ako zelite upisite da", travel.Item5);
                                        if ((Console.ReadLine() ?? "").Trim().ToLower() == "da")
                                        {
                                            double edit_fuel_price = 0;
                                            double edit_total = 0;
                                            while (true)
                                            {
                                                Console.WriteLine("Unesite novu cijenu goriva po litri na putovanju ");
                                                if (double.TryParse(Console.ReadLine(), out edit_fuel_price) && edit_fuel_price > 0)
                                                {   
                                                    edit_total = travel.Item4 * edit_fuel_price;
                                                    var updated_travel = (travel.Item1, travel.Item2, travel.Item3, travel.Item4, edit_fuel_price, edit_total);
                                                    all_travels[i] = updated_travel;
                                                    break;
                                                }
                                                else Console.WriteLine("Potrebno je unijeti broj ");
                                            }
                                            Console.WriteLine("Promijenuta je cijena po litri goriva na putovanju u {0}", edit_fuel_price);
                                        }
                                    }
                                }
                                Console.WriteLine("Uredjivanje putovanja je zavrseno! ");
                                break;
                            }
                            else Console.WriteLine("Treba bit broj");
                        }
                        break;
                    case 4:
                        Console.WriteLine("Ispis svih putovanja\n");
                        Console.WriteLine("1 - Sva putovanja redom kako su spremljena\n2 - Sva putovanja sortirana po trošku uzlazno \r\n3 - Sva  putovanja sortirana po trošku silazno \r\n4 - Sva  putovanja sortirana po kilometraži uzlazno \r\n5 - Sva  putovanja sortirana po kilometraži silazno \r\n6 - Sva  putovanja sortirana po datumu uzlazno \r\n7 - Sva  putovanja sortirana po datumu silazno ");
                        int view_choice = -1;

                        while (view_choice != 1 && view_choice != 2 && view_choice != 3 && view_choice != 4 && view_choice != 5 && view_choice != 6 && view_choice != 7)
                        {
                            Console.Write("Unesite zeljeni odabir: ");
                            if (int.TryParse(Console.ReadLine(), out view_choice))
                            {
                                Console.WriteLine("Potrebno je unijeti jedan od ponudjenih brojeva");
                            }
                            else Console.WriteLine("Potrebno je unijeti broj! ");
                        }
                        if (view_choice == 1)
                        {
                            Console.WriteLine("Sva putovanja redom kako su spremljena");
                            foreach (var user in users)
                            {
                                var all_travels = (List<(string, DateTime, double, double, double, double)>)user.Value[3];
                                foreach (var travel in all_travels)
                                {
                                    Console.WriteLine($"{travel.Item1}\nDatum: {travel.Item2.ToString("yyyy-MM-dd")}\nKilometri: {travel.Item3}\nGorivo: {travel.Item4} L\nCijena po litri: {travel.Item5.ToString("0.00", CultureInfo.InvariantCulture)} EUR\nUkupno: {travel.Item6.ToString("0.00", CultureInfo.InvariantCulture)} EUR\n");
                                    //za . umjesto ,
                                }
                            }
                        }
                        if (view_choice == 2)
                        {
                            Console.WriteLine("Sva putovanja sortirana po trošku uzlazno");
                            var travels_from_all_users = new List<(string, DateTime, double, double, double, double)>();
                            foreach (var user in users)
                            {
                                var travels_from_one_user = (List<(string, DateTime, double, double, double, double)>)user.Value[3];
                                travels_from_all_users.AddRange(travels_from_one_user); //ne moze add
                            }
                            travels_from_all_users = travels_from_all_users.OrderBy(o => o.Item6).ToList();
                            foreach (var travel in travels_from_all_users)
                            {
                                Console.WriteLine($"{travel.Item1}\nDatum: {travel.Item2.ToString("yyyy-MM-dd")}\nKilometri: {travel.Item3}\nGorivo: {travel.Item4} L\nCijena po litri: {travel.Item5.ToString("0.00", CultureInfo.InvariantCulture)} EUR\nUkupno: {travel.Item6.ToString("0.00", CultureInfo.InvariantCulture)} EUR\n");
                            }                        
                        }
                        else if(view_choice == 3)
                        {
                            Console.WriteLine("Sva  putovanja sortirana po trošku silazno");
                            var travels_from_all_users = new List<(string, DateTime, double, double, double, double)>();
                            foreach (var user in users)
                            {
                                var travels_from_one_user = (List<(string, DateTime, double, double, double, double)>)user.Value[3];
                                travels_from_all_users.AddRange(travels_from_one_user);
                            }
                            travels_from_all_users = travels_from_all_users.OrderByDescending(o => o.Item6).ToList();
                            foreach (var travel in travels_from_all_users)
                            {
                                Console.WriteLine($"{travel.Item1}\nDatum: {travel.Item2.ToString("yyyy-MM-dd")}\nKilometri: {travel.Item3}\nGorivo: {travel.Item4} L\nCijena po litri: {travel.Item5.ToString("0.00", CultureInfo.InvariantCulture)} EUR\nUkupno: {travel.Item6.ToString("0.00", CultureInfo.InvariantCulture)} EUR\n");
                            }
                        }
                        else if(view_choice == 4)
                        {
                            Console.WriteLine("Sva putovanja sortirana po kilometrazi uzlazno");
                            var travels_from_all_users = new List<(string, DateTime, double, double, double, double)>();
                            foreach (var user in users)
                            {
                                var travels_from_one_user = (List<(string, DateTime, double, double, double, double)>)user.Value[3];
                                travels_from_all_users.AddRange(travels_from_one_user);
                            }
                            travels_from_all_users = travels_from_all_users.OrderBy(o => o.Item3).ToList();
                            foreach (var travel in travels_from_all_users)
                            {
                                Console.WriteLine($"{travel.Item1}\nDatum: {travel.Item2.ToString("yyyy-MM-dd")}\nKilometri: {travel.Item3}\nGorivo: {travel.Item4} L\nCijena po litri: {travel.Item5.ToString("0.00", CultureInfo.InvariantCulture)} EUR\nUkupno: {travel.Item6.ToString("0.00", CultureInfo.InvariantCulture)} EUR\n");
                            }
                        }
                        else if (view_choice == 5)
                        {
                            Console.WriteLine("Sva putovanja sortirana po kilometrazi silazno");
                            var travels_from_all_users = new List<(string, DateTime, double, double, double, double)>();
                            foreach (var user in users)
                            {
                                var travels_from_one_user = (List<(string, DateTime, double, double, double, double)>)user.Value[3];
                                travels_from_all_users.AddRange(travels_from_one_user);
                            }
                            travels_from_all_users = travels_from_all_users.OrderByDescending(o => o.Item3).ToList();
                            foreach (var travel in travels_from_all_users)
                            {
                                Console.WriteLine($"{travel.Item1}\nDatum: {travel.Item2.ToString("yyyy-MM-dd")}\nKilometri: {travel.Item3}\nGorivo: {travel.Item4} L\nCijena po litri: {travel.Item5.ToString("0.00", CultureInfo.InvariantCulture)} EUR\nUkupno: {travel.Item6.ToString("0.00", CultureInfo.InvariantCulture)} EUR\n");
                            }
                        }
                        else if (view_choice == 6)
                        {
                            Console.WriteLine("Sva putovanja sortirana po datumu uzlazno");
                            var travels_from_all_users = new List<(string, DateTime, double, double, double, double)>();
                            foreach (var user in users)
                            {
                                var travels_from_one_user = (List<(string, DateTime, double, double, double, double)>)user.Value[3];
                                travels_from_all_users.AddRange(travels_from_one_user);
                            }
                            travels_from_all_users = travels_from_all_users.OrderBy(o => o.Item2).ToList();
                            foreach (var travel in travels_from_all_users)
                            {
                                Console.WriteLine($"{travel.Item1}\nDatum: {travel.Item2.ToString("yyyy-MM-dd")}\nKilometri: {travel.Item3}\nGorivo: {travel.Item4} L\nCijena po litri: {travel.Item5.ToString("0.00", CultureInfo.InvariantCulture)} EUR\nUkupno: {travel.Item6.ToString("0.00", CultureInfo.InvariantCulture)} EUR\n");
                            }
                        }
                        else if (view_choice == 7)
                        {
                            Console.WriteLine("Sva putovanja sortirana po datumu silazno");
                            var travels_from_all_users = new List<(string, DateTime, double, double, double, double)>();
                            foreach (var user in users)
                            {
                                var travels_from_one_user = (List<(string, DateTime, double, double, double, double)>)user.Value[3];
                                travels_from_all_users.AddRange(travels_from_one_user);
                            }
                            travels_from_all_users = travels_from_all_users.OrderByDescending(o => o.Item2).ToList();
                            foreach (var travel in travels_from_all_users)
                            {
                                Console.WriteLine($"{travel.Item1}\nDatum: {travel.Item2.ToString("yyyy-MM-dd")}\nKilometri: {travel.Item3}\nGorivo: {travel.Item4} L\nCijena po litri: {travel.Item5.ToString("0.00", CultureInfo.InvariantCulture)} EUR\nUkupno: {travel.Item6.ToString("0.00", CultureInfo.InvariantCulture)} EUR\n");
                            }
                        }
                        break;
                    case 5:
                        Console.WriteLine("Izvještaji i analize");
                        while (true)
                        {
                            Console.WriteLine("\nUnesite id korisnika cije vas izvjesce zanima: ");
                            if (int.TryParse(Console.ReadLine(), out var user_report))
                            {
                                if (users.ContainsKey(user_report))
                                {
                                    var user = users[user_report];
                                    var user_travels = (List<(string, DateTime, double, double, double, double)>)user[3];

                                    Console.WriteLine("1 - Ukupna potrošnja goriva (zbroj svih litara) \r\n2 - Ukupni troškovi goriva (zbroj svih goriva * cijena) \r\n3 - Prosječna potrošnja goriva u L/100km po formuli: \r\nprosjek = (ukupno_gorivo / ukupno_kilometara) * 100 \r\n4 - Putovanje s najvećom potrošnjom goriva \r\n5 - Pregled putovanja po određenom datumu ");
                                    int choice_of_report = -1;

                                    while (choice_of_report != 1 && choice_of_report != 2 && choice_of_report != 3 && choice_of_report != 4 && choice_of_report != 5 )
                                    {
                                        Console.Write("Unesite zeljeni odabir: ");
                                        if (int.TryParse(Console.ReadLine(), out choice_of_report))
                                        {
                                            Console.WriteLine("Potrebno je unijeti jedan od ponudjenih brojeva");
                                        }
                                        else Console.WriteLine("Potrebno je unijeti broj! ");
                                    }

                                    if (choice_of_report == 1)
                                    {
                                        double total_fuel_consumption = 0;
                                        foreach (var travel in user_travels)
                                        {
                                            total_fuel_consumption += travel.Item4;
                                        }
                                        Console.WriteLine("Korisnik je sveukupno potrosio {0} L goriva.", total_fuel_consumption);
                                    }
                                    else if (choice_of_report == 2)
                                    {
                                        double total_of_total_costs = 0;
                                        foreach (var travel in user_travels)
                                        {
                                            total_of_total_costs += travel.Item6;
                                        }
                                        Console.WriteLine("Korisnikov sveukupni trosak je {0}.", total_of_total_costs);
                                    }
                                    else if (choice_of_report == 3)
                                    {
                                        double avg_fuel_consumption = 0;
                                        double total_fuel = 0;
                                        double total_km = 0;
                                        foreach (var travel in user_travels)
                                        {
                                            total_fuel += travel.Item4;
                                            total_km += travel.Item3;
                                            avg_fuel_consumption =(total_fuel/total_km)*100;
                                        }
                                        Console.WriteLine("Korisnikova prosječna potrošnja goriva u L/100km je {0}.", avg_fuel_consumption);
                                    }
                                    else if (choice_of_report == 4)
                                    {
                                        double biggest_fuel_consumption = 0;
                                        var travel_with_biggest_fuel_consumption=("", DateTime.MinValue, 0.0, 0.0, 0.0, 0.0);
                                        foreach (var travel in user_travels)
                                        {   if(travel.Item4 > biggest_fuel_consumption)
                                            {
                                                biggest_fuel_consumption = travel.Item4;
                                                travel_with_biggest_fuel_consumption = travel;
                                            }      

                                        }
                                        Console.WriteLine("Najveci trosak goriva je od {0}.", biggest_fuel_consumption);
                                        Console.WriteLine("Ono je na putovanju:");
                                        Console.WriteLine($"{travel_with_biggest_fuel_consumption.Item1}\nDatum: {travel_with_biggest_fuel_consumption.Item2.ToString("yyyy-MM-dd")}\nKilometri: {travel_with_biggest_fuel_consumption.Item3}\nGorivo: {travel_with_biggest_fuel_consumption.Item4} L\nCijena po litri: {travel_with_biggest_fuel_consumption.Item5.ToString("0.00", CultureInfo.InvariantCulture)} EUR\nUkupno: {travel_with_biggest_fuel_consumption.Item6.ToString("0.00", CultureInfo.InvariantCulture)} EUR\n");
                                    }
                                    else if(choice_of_report == 5)
                                    {
                                        DateTime search_by_date;
                                        while (true)
                                        {
                                            Console.Write("Unesite datum po kejemu zelite pregledat putovanja (YYYY-MM-DD) ");
                                            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out search_by_date))
                                            {
                                                break;
                                            }
                                            else { Console.WriteLine("Niste unijeli datum u ispravnom formatu "); }
                                        }
                                        foreach (var travel in user_travels)
                                        {
                                            if (travel.Item2 == search_by_date)
                                            {
                                                Console.WriteLine($"{travel.Item1}\nDatum: {travel.Item2.ToString("yyyy-MM-dd")}\nKilometri: {travel.Item3}\nGorivo: {travel.Item4} L\nCijena po litri: {travel.Item5.ToString("0.00", CultureInfo.InvariantCulture)} EUR\nUkupno: {travel.Item6.ToString("0.00", CultureInfo.InvariantCulture)} EUR\n");

                                            }
                                        }
                                    }
                                    break;
                                }
                                else Console.WriteLine("Ne postoji korisnik s unesenim IDiem.");
                            }
                            else Console.WriteLine("Trebate unijeti broj! ");
                        }
                        break;
                }
            }
        }

    }
}
