using System;
using System.Runtime.InteropServices;

namespace practicalWorkI
{
    class Program
    {
        public static void PrintMenu()
        {

            Console.WriteLine("===============================================");
            Console.WriteLine("||   Welcome to the UFV Train Station        ││");
            Console.WriteLine("││                                           ││");
            Console.WriteLine("││   Choose an option:                       ││");
            Console.WriteLine("││   1. Load Trains from file.               ││");
            Console.WriteLine("││   2. Start simulation.                    ││");
            Console.WriteLine("││   3. Exit.                                ││");
            Console.WriteLine("││                                           ││");
            Console.WriteLine("===============================================");
            Console.WriteLine();
            Console.WriteLine();

        }
        static void Main(string[] args)
        {
            int numPlatforms = 0;
            int option = 0;

            Console.WriteLine("Enter the number of platforms to create");
            numPlatforms = Int32.Parse(Console.ReadLine());

            Station station = new Station("UFV Train Station", numPlatforms);

            while (option != 3)
            {
                PrintMenu();
                option = Int32.Parse(Console.ReadLine());

                if (option == 1)
                {
                    station.LoadTrainsFromFile();
                }
                if (option == 2)
                {
                    station.StartSimulation();
                }
                if (option <= 0 || option >= 4)
                {
                    Console.WriteLine("Error. Choose an option between 1 and 3");
                }
            }
            return;
        }
    }
}