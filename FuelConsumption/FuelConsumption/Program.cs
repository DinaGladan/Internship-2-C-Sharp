using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
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
                      { ("Bih", new DateTime(2020,03,30), 300, 40, 1.82, 40*1.82),
                        ("Cro", new DateTime(2020,04,20), 300, 40, 1.82, 40*1.82)
                      }
                    }
                },
                {555,
                    new List<object>
                    { "Ivan",
                      "Gvcevic",
                      new DateTime(2004, 01, 10),
                      new List<(string, DateTime, double, double, double, double)>
                      { ("Slo", new DateTime(2020,03,30), 300, 40, 1.82, 40*1.82)
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
                        break;
                    case 5:
                        break;
                }
            }
        }

    }
}
