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
                        Users();
                        break;
                    case 2:
                        Travels();
                        break;
                }
            }


        }

        static void Users() {
            Console.WriteLine("Korisnici: ");
        }

        static void Travels()
        {
            Console.WriteLine("Putovanja: ");
        }

    }
}
