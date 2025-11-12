using System.Globalization;

namespace FuelConsumption
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("2 - Korisnici");
                Console.WriteLine("1 - Putovanja");
                Console.WriteLine("0 - Izlaz iz aplikacije");
                var firstChoice = 0;
                do
                {
                    Console.WriteLine("Unesite zeljenu opciju (0,1,2): ");
                    if (int.TryParse(Console.ReadLine(), out firstChoice))
                    {
                        Console.WriteLine("Unijeli ste broj {0}", firstChoice);
                    }
                    else Console.WriteLine("Potrebno je unijeti broj");

                } while (firstChoice != 0 && firstChoice != 1 && firstChoice != 2);

                switch (firstChoice)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        Travels();
                        break;
                    case 2:
                        Users_function();
                        break;
                }
            }


        }

        static void Users_function() {
            Console.WriteLine("Korisnici: ");
            Console.WriteLine("1 - Unos novog korisnika \r\n2 - Brisanje korisnika \r\n3 - Uređivanje korisnika \r\n4 - Pregled svih korisnika \r\n0 - Povratak na glavni izbornik");
            int user_choice =-1;
            var users = new Dictionary<int, List<object>>
            {
                {575, new List<object> { "Dina", "Gladan", new DateTime(2004,02,20), new List<string> { "Bih", "Cro", "Slo" } } },
                {555, new List<object> { "Ivan", "Ivcevic", new DateTime(2004, 01, 10), new List<string> { "Srb", "Cro" } } }
            };


            while (user_choice != 0 && user_choice != 1 && user_choice != 2 && user_choice != 3 && user_choice != 4)
            {
                Console.Write("Unesite zeljeni odabir: ");
                if( int.TryParse(Console.ReadLine(), out user_choice))
                {
                    Console.WriteLine("Potrebno je unijeti jedan od ponudjenih brojeva");
                }
                else Console.WriteLine("Potrebno je unijeti broj! ");
            }

            switch (user_choice)
            {
                case 0:
                    //vrati na main
                    break;
                case 1:
                    Console.WriteLine("Dodavanje novog korisnika: ");
                    int new_user_id;
                    while (true)
                    {
                        Console.Write("UUnesite id korisnika ");
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
                    while (true) {
                        Console.Write("Unesite datum rodjenja korisnika (YYYY-MM-DD) ");
                        if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd",CultureInfo.InvariantCulture,DateTimeStyles.None, out new_user_birth_date))
                        { //mala slova za godinu i dane!!
                            break;
                        }
                        else { Console.WriteLine("Niste unijeli datum u ispravnom formatu "); }

                    }

                    Console.WriteLine("Ako zelite dodat putovanja vratite se na glavni izbornik.");
                    var new_user_travels = new List<string>();

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
                                if(users.ContainsKey(deleteByID)){
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
                                users[edit_user] = new List<object>{edit_name,edit_surname, edit_birth_date, old_travels };
                                Console.WriteLine("Uredili smo korisnika s IDiem {0}", edit_user);
                                break;
                            }
                            else Console.WriteLine("Korisnik s unesenim ID-iem ne postji");
                        }
                        else Console.WriteLine("Treba bit broj");
                    }
                    break;
                case 4:

                    break;
            }

        }

        static void Travels()
        {
            Console.WriteLine("Putovanja: ");
        }

    }
}
