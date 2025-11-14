using System.Collections.Generic;
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
                      new DateTime(2004,02,20),
                      new List<(string, DateTime, double, double, double, double)>
                      { ("Imotski", new DateTime(2022, 5, 12), 128.4, 9.2, 1.63, 9.2 * 1.63),
                        ("Jezero", new DateTime(2025, 6, 6), 22.7, 1.6, 1.42, 1.6 * 1.42),
                        ("Makarska", new DateTime(2022, 7, 19), 312.9, 21.8, 1.78, 21.8 * 1.78),
                        ("Omiš", new DateTime(2024, 8, 2), 14.3, 1.1, 1.54, 1.1 * 1.54),
                        ("Zagreb", new DateTime(2023, 9, 5), 456.2, 32.5, 1.90, 32.5 * 1.90)
                      }
                    }
                },
                {555,
                    new List<object>
                    { "Ivan",
                      "Gvcevic",
                      new DateTime(1994, 01, 10),
                      new List<(string, DateTime, double, double, double, double)>
                      { ("Italija", new DateTime(2024, 10, 12), 380.0, 26.5, 1.85, 26.5 * 1.85),
                        ("Slovenija", new DateTime(2024, 11, 5), 470.0, 31.8, 1.92, 31.8 * 1.92),
                        ("Austrija", new DateTime(2024, 12, 1), 690.0, 46.0, 1.95, 46.0 * 1.95),
                        ("Bosna i Hercegovina", new DateTime(2025, 1, 22), 170.0, 11.4, 1.60, 11.4 * 1.60),
                        ("Crna Gora", new DateTime(2025, 6, 6), 290.0, 19.2, 1.70, 19.2 * 1.70)
                      }
                    }
                },
                {645,
                    new List<object>
                    { "Dina",
                      "Nncevic",
                      new DateTime(2001,11,11),
                      new List<(string, DateTime, double, double, double, double)>
                      { ("Mađarska", new DateTime(2025, 3, 10), 760.0, 49.5, 1.94, 49.5 * 1.94),
                        ("Srbija", new DateTime(2025, 4, 2), 640.0, 41.7, 1.88, 41.7 * 1.88),
                        
                        ("Češka", new DateTime(2025, 6, 6), 980.0, 64.2, 1.97, 64.2 * 1.97), 
                        ("Slovačka", new DateTime(2025, 6, 6), 920.0, 59.0, 1.96, 59.0 * 1.96)
                      }
                    }
                }
            };
            while (true)
            {
                Console.WriteLine("1 - Korisnici");
                Console.WriteLine("2 - Putovanja");
                Console.WriteLine("3 - Statistika");
                Console.WriteLine("0 - Izlaz iz aplikacije");
                var firstChoice = -1;
                do
                {
                    Console.Write("Unesite zeljenu opciju (0,1,2,3): ");
                    if (int.TryParse(Console.ReadLine(), out firstChoice))
                        Console.WriteLine(); 
                    else {
                        firstChoice = -1;// ne shvacam zasto mi inace postavi na 0, ako upisem slovo
                        Console.WriteLine("Potrebno je unijeti broj");
                    }
                    

                } while (firstChoice != 0 && firstChoice != 1 && firstChoice != 2 && firstChoice != 3);

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
                    case 3:
                        Statistics(users);
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
                        Console.WriteLine();
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
                            Console.Write("Unesite id korisnika: ");
                            if (int.TryParse(Console.ReadLine(), out new_user_id))
                            {
                                break;
                            }
                            else Console.WriteLine("Trebate unijeti broj za id ");
                        }
                        string new_user_name = "";
                        do
                        {
                            Console.Write("Unesite ime korisnika: ");
                            new_user_name = Console.ReadLine();
                        } while (string.IsNullOrEmpty(new_user_name) || double.TryParse(new_user_name, out double number));

                        string new_user_surname = "";
                        do
                        {
                            Console.Write("Unesite prezime korisnika: ");
                            new_user_surname = Console.ReadLine();
                        } while (string.IsNullOrEmpty(new_user_surname) || double.TryParse(new_user_surname, out double number));

                        DateTime new_user_birth_date;

                        while (true)
                        {
                            Console.Write("Unesite datum rodjenja korisnika (YYYY-MM-DD) ");
                            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out new_user_birth_date) && new_user_birth_date <DateTime.Now)
                            { //mala slova za godinu i dane!!
                                break;
                            }
                            else Console.WriteLine("Niste unijeli datum u ispravnom formatu ili ste unijeli previsoku godinu ");
                        }

                        Console.WriteLine("Ako zelite dodat putovanja vratite se na glavni izbornik.\n");
                        var new_user_travels = new List<(string, DateTime, double, double, double, double)>();

                        users.Add(new_user_id, new List<object> { new_user_name, new_user_surname, new_user_birth_date, new_user_travels });
                        break;

                    case 2:

                        var deleteChoice = 0;
                        while (deleteChoice != 1 && deleteChoice != 2)
                        {
                            Console.WriteLine("Zelite li obrisati korisnika: \n 1 - po ID-u\n 2 - po imenu ");
                            if (int.TryParse(Console.ReadLine(), out deleteChoice))
                                Console.WriteLine();
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
                                        Console.Write("Jeste li sigurni da zelite izbrisat unesenog korisnika? (da/ne) ");
                                        if ((Console.ReadLine() ?? "").Trim().ToLower() == "da")
                                        {
                                            users.Remove(deleteByID);
                                            Console.WriteLine("Izbrisali smo korisnika s IDiem {0}\n", deleteByID);
                                        }
                                        else Console.WriteLine("Korisnik se ipak nece izbrisat! ");
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
                            string deleteBySurname = "";

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

                                Console.Write("Unesite prezime korisnika kojeg zelite izbrisat ");
                                deleteBySurname = Console.ReadLine();

                                if (string.IsNullOrEmpty(deleteBySurname))
                                {
                                    Console.WriteLine("Ne mozete ostavit prazno");
                                    continue;
                                }

                                foreach (var id in users.Keys)
                                {
                                    if (users[id][0].ToString().Equals(deleteByName, StringComparison.OrdinalIgnoreCase) && users[id][1].ToString().Equals(deleteBySurname, StringComparison.OrdinalIgnoreCase))
                                    //potrebno ako je razlika u malim i velikim slovima
                                    {   
                                        keyForDelete = id;
                                        break;
                                    }
                                }
                                if (keyForDelete != 0)
                                {
                                    Console.Write("Jeste li sigurni da zelite izbrisat unesenog korisnika? (da/ne) ");
                                    if ((Console.ReadLine() ?? "").Trim().ToLower() == "da")
                                    {
                                        users.Remove(keyForDelete);
                                        Console.WriteLine("Izbrisali smo korisnika s imenom i prezimenom {0} {1}", deleteByName, deleteBySurname);
                                    }
                                    else Console.WriteLine("Korisnik se ipak nece izbrisat! ");
                                    break;
                                }
                                else Console.WriteLine("Ne postoji uneseni korisnik!");
                            }
                        }
                        break;

                    case 3:
                        int edit_user = 0;
                        while (true)
                        {
                            Console.Write("Unesite id korisnika kojeg zelite urediti ");
                            if (int.TryParse(Console.ReadLine(), out edit_user))
                            {
                                if (users.ContainsKey(edit_user))
                                {
                                    string edit_name = "";
                                    do
                                    {
                                        Console.Write("Unesite ime korisnika: ");
                                        edit_name = Console.ReadLine();
                                    } while (string.IsNullOrEmpty(edit_name) || double.TryParse(edit_name, out double number));

                                    string edit_surname = "";
                                    do
                                    {
                                        Console.Write("Unesite prezime korisnika: ");
                                        edit_surname = Console.ReadLine();
                                    } while (string.IsNullOrEmpty(edit_surname) || double.TryParse(edit_surname, out double number));

                                    DateTime edit_birth_date;
                                    while (true)
                                    {
                                        Console.Write("Unesite datum rodjenja korisnika (YYYY-MM-DD) ");
                                        if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out edit_birth_date) && edit_birth_date < DateTime.Now)
                                        {
                                            break;
                                        }
                                        else { Console.WriteLine("Niste unijeli datum u ispravnom formatu ili ste unijeli previsoku godinu "); }

                                    }
                                    var old_travels = (List<(string, DateTime, double, double, double, double)>)users[edit_user][3]; //treba castat jer u suprotnom vraca objekt

                                    Console.Write("Jeste li sigurni da zelite uredit unesenog korisnika? (da/ne) ");
                                    if ((Console.ReadLine() ?? "").Trim().ToLower() == "da")
                                    {
                                        users[edit_user] = new List<object> { edit_name, edit_surname, edit_birth_date, old_travels };
                                        Console.WriteLine("Uredili smo korisnika s IDiem {0}\n", edit_user);
                                    }
                                    else Console.WriteLine("Korisnik se ipak nece uredit!\n ");
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
                                Console.WriteLine();
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
                            Console.WriteLine();

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
                            Console.WriteLine();
                        }
                        else if (viewChoice == 3)
                        {
                            foreach (var key_value_pair in users)
                            {
                                var id = key_value_pair.Key;
                                var name = key_value_pair.Value[0].ToString();
                                var surname = key_value_pair.Value[1].ToString();
                                var birthdate = (DateTime)key_value_pair.Value[2];
                                var travels = (List<(string, DateTime, double, double, double, double)>)key_value_pair.Value[3];

                                if (travels.Count >= 2)
                                {
                                    Console.WriteLine($"{id} - {name} - {surname} - {((DateTime)birthdate).ToShortDateString()}");
                                }
                            }
                            Console.WriteLine();
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
                        Console.WriteLine();
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
                            Console.Write("Unesite id korisnika kojem zelite dodat putovanje ");
                            if (int.TryParse(Console.ReadLine(), out var userID))
                            {
                                if (users.ContainsKey(userID))
                                {
                                    Console.Write("Unesite naziv putovanja "); var new_travel_name = Console.ReadLine();
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
                                        Console.Write("Unesite prijedjenu kilometrazu: ");
                                        if (double.TryParse(Console.ReadLine(), out new_distance) && new_distance > 0)
                                        {
                                            break;
                                        }
                                        else Console.WriteLine("Potrebno je unijeti pozitivan broj ");
                                    }
                                    double new_spent_fuel = 0;
                                    while (true)
                                    {
                                        Console.Write("Unesite iznos potrosenog goriva: ");
                                        if (double.TryParse(Console.ReadLine(), out new_spent_fuel) && new_spent_fuel > 0)
                                        {
                                            break;
                                        }
                                        else Console.WriteLine("Potrebno je unijeti pozitivan broj ");
                                    }
                                    double new_fuel_price = 0;
                                    while (true)
                                    {
                                        Console.Write("Unesite cijenu litre goriva: ");
                                        if (double.TryParse(Console.ReadLine(), out new_fuel_price) && new_fuel_price > 0)
                                        {
                                            break;
                                        }
                                        else Console.WriteLine("Potrebno je unijeti pozitivan broj ");
                                    }
                                    var new_total = new_spent_fuel * new_fuel_price;
                                    var new_travel = (new_travel_name, new_travel_date, new_distance, new_spent_fuel, new_fuel_price, new_total);

                                    ((List<(string, DateTime, double, double, double, double)>)users[userID][3]).Add(new_travel);
                                    Console.WriteLine();
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
                                Console.WriteLine();
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
                                        Console.Write("Jeste li sigurni da zelite izbrisat putovanja unesenog korisnika? (da/ne) ");
                                        if ((Console.ReadLine() ?? "").Trim().ToLower() == "da")
                                        {
                                            ((List<(string, DateTime, double, double, double, double)>)users[deleteByID][3]).Clear();
                                            Console.WriteLine("Izbrisali smo sva putovanja korisniku s IDiem {0}", deleteByID);
                                        }
                                        else Console.WriteLine("Korisniku se ipak nece izbrisat putovanja!\n ");
                                        Console.WriteLine();
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
                                        Console.Write("Jeste li sigurni da zelite izbrisat putovanja iznad unesenog iznosa za korisnika s ID: {0}? (da/ne) ", keyValuePair.Key);
                                        if ((Console.ReadLine() ?? "").Trim().ToLower() == "da")
                                        {
                                            var all_travels = (List<(string, DateTime, double, double, double, double)>)keyValuePair.Value[3];
                                            int remove = all_travels.RemoveAll(travel => travel.Item6 > largest_amount);
                                            if (remove > 0)
                                            {
                                                Console.WriteLine("Korisniku s IDiem: {0} je obrisano {1} putovanja.", keyValuePair.Key, remove);
                                            }
                                            else Console.WriteLine("Nije obrisano niti jedno putovanje");
                                        }
                                        else Console.WriteLine("Korisniku se ipak nece izbrisat putovanja!\n ");
                                        Console.WriteLine();
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
                                        Console.Write("Jeste li sigurni da zelite izbrisat putovanja ispod unesenog iznosa za korisnika s ID: {0}? (da/ne) ", keyValuePair.Key);
                                        if ((Console.ReadLine() ?? "").Trim().ToLower() == "da")
                                        {
                                            var all_travels = (List<(string, DateTime, double, double, double, double)>)keyValuePair.Value[3];
                                            int remove = all_travels.RemoveAll(travel => travel.Item6 < lowest_amount);
                                            if (remove > 0)
                                            {
                                                Console.WriteLine("Korisniku s IDiem: {0} je obrisano {1} putovanja.", keyValuePair.Key, remove);
                                            }
                                            else Console.WriteLine("Nije obrisano niti jedno putovanje");
                                        }
                                        else Console.WriteLine("Korisniku se ipak nece izbrisat putovanja!\n ");
                                        Console.WriteLine();
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
                            Console.Write("Unesite id korisnika cija putovanja zelite urediti ");
                            if (int.TryParse(Console.ReadLine(), out edit_user_travels))
                            {
                                if (users.ContainsKey(edit_user_travels))
                                {
                                    var all_travels = (List<(string, DateTime, double, double, double, double)>)users[edit_user_travels][3];
                                    for (int i = 0; i < all_travels.Count; i++)
                                    {
                                        var travel = all_travels[i];
                                        Console.Write("Zelite li promijeniti naziv putovanja, trenutno je {0}\nako zelite upisite da ", travel.Item1);
                                        if ((Console.ReadLine() ?? "").Trim().ToLower() == "da")
                                        {
                                            Console.WriteLine("Unesite novi naziv putovanja ");
                                            var edit_travel_name = Console.ReadLine();
                                            edit_travel_name = string.IsNullOrWhiteSpace(edit_travel_name) ? travel.Item1 : edit_travel_name;
                                            var updated_travel = (edit_travel_name, travel.Item2, travel.Item3, travel.Item4, travel.Item5, travel.Item6);
                                            all_travels[i] = updated_travel;
                                            Console.WriteLine("Promijenut je naziv putovanja u {0}", edit_travel_name);
                                        }
                                       
                                        Console.Write("Zelite li promijeniti datum putovanja, trenutno je {0}\nako zelite upisite da ", travel.Item2.ToShortDateString());
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
                                                    Console.WriteLine("Promijenut je datum putovanja u {0}", edit_travel_date.ToShortDateString());
                                                    break;
                                                }
                                                else  Console.WriteLine("Niste unijeli datum u ispravnom formatu ");
                                            }
                                        }
                                        Console.Write("Zelite li promijeniti prjedjenu kilometrazu na putovanju, trenutno je {0}\nako zelite upisite da ", travel.Item3);
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
                                        Console.Write("Zelite li promijeniti iznos potrosenog goriva na putovanju, trenutno je {0}\nako zelite upisite da ", travel.Item4);
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
                                        Console.Write("Zelite li promijeniti cijenu po litri potrosenog goriva na putovanju, trenutno je {0}\nako zelite upisite da ", travel.Item5);
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
                        Console.WriteLine();
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
                            Console.WriteLine();
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
                            Console.WriteLine();
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
                            Console.WriteLine();
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
                            Console.WriteLine();
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
                            Console.WriteLine();
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
                            Console.WriteLine();
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
                            Console.WriteLine();
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
                                            Console.WriteLine();
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
                                        Console.WriteLine("Korisnik je sveukupno potrosio {0} L goriva.\n", total_fuel_consumption);
                                    }
                                    else if (choice_of_report == 2)
                                    {
                                        double total_of_total_costs = 0;
                                        foreach (var travel in user_travels)
                                        {
                                            total_of_total_costs += travel.Item6;
                                        }
                                        Console.WriteLine("Korisnikov sveukupni trosak je {0}\n.", total_of_total_costs);
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
                                        Console.WriteLine("Korisnikova prosječna potrošnja goriva u L/100km je {0}\n.", Math.Round(avg_fuel_consumption,2));
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

        static void Statistics(Dictionary<int, List<object>> users)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Statistika: ");
                Console.WriteLine("1 - Korisnik s najvećim ukupnim troškom goriva \r\n2 - Korisnik s najviše putovanja \r\n3 - Prosječan broj putovanja po korisniku \r\n4 - Ukupan broj prijeđenih kilometara svih korisnika \r\n0 - Povratak na glavni izbornik");
                int statistics_choice = -1;

                while (statistics_choice != 0 && statistics_choice != 1 && statistics_choice != 2 && statistics_choice != 3 && statistics_choice != 4)
                {
                    Console.Write("Unesite zeljeni odabir: ");
                    if (int.TryParse(Console.ReadLine(), out statistics_choice))
                        Console.WriteLine();
                    else Console.WriteLine("Potrebno je unijeti broj! ");
                }
                switch (statistics_choice)
                {
                    case 0:
                        exit = true;
                        break;

                    case 1:
                        double highest_total_fuel_cost = 0;
                        var user_with_highest_total_fuel_cost = new KeyValuePair<int, List<object>>();
                        foreach (var user in users)
                        {
                            double one_user_total_fuel_cost = 0;
                            var travels_from_one_user = (List<(string, DateTime, double, double, double, double)>)user.Value[3];
                            foreach (var travel in travels_from_one_user)
                            {
                                one_user_total_fuel_cost += travel.Item6;
                            }
                            if (one_user_total_fuel_cost > highest_total_fuel_cost) { 
                                highest_total_fuel_cost = one_user_total_fuel_cost;
                                user_with_highest_total_fuel_cost = user;
                            }
                        }
                        Console.WriteLine("ID korisnika s najvecim ukupnim troskom goriva je: {0}\nU iznosu od {1}", user_with_highest_total_fuel_cost.Key,highest_total_fuel_cost);
                        break;

                    case 2:
                        double most_travels = 0;
                        var user_with_most_travels = new KeyValuePair<int, List<object>>();
                        foreach (var user in users)
                        {
                            var travels_from_one_user = (List<(string, DateTime, double, double, double, double)>)user.Value[3];
                            int user_travels = travels_from_one_user.Count();
                            if (user_travels > most_travels)
                            {
                                user_with_most_travels = user;
                                most_travels = user_travels;
                            }
                        }
                        Console.WriteLine("ID korisnika s najvecim ubrojem putovanja je: {0}\nU iznosu od {1}", user_with_most_travels.Key, most_travels);
                        break;

                    case 3:
                        var travels_from_all_users = new List<(string, DateTime, double, double, double, double)>();
                        var number_of_users = users.Count();
                        foreach (var user in users)
                        {
                            var travels_from_one_user = (List<(string, DateTime, double, double, double, double)>)user.Value[3];
                            travels_from_all_users.AddRange(travels_from_one_user);
                        }
                        var travels_count = travels_from_all_users.Count();
                        Console.WriteLine("Prosjecan broj putovanja po korisniku je {0}", Math.Round((double)travels_count/number_of_users,2));
                        break;

                    case 4:
                        break;

                }
            }
        }
    }
}
