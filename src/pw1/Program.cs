using System;
using System.Runtime.InteropServices;

namespace practicalWorkI
{
    class Program
    {
        // Method to print the menu options
        public static void PrintMenu()
        {

            Console.WriteLine("===============================================");
            Console.WriteLine("||   Welcome to the UFV Train Station        ││");
            Console.WriteLine("││                                           ││");
            Console.WriteLine("││   Choose an option:                       ││");
            Console.WriteLine("││   1. Load Trains from CSV file.               ││");
            Console.WriteLine("││   2. Start simulation.                    ││");
            Console.WriteLine("││   3. Exit.                                ││");
            Console.WriteLine("││                                           ││");
            Console.WriteLine("===============================================");
            Console.WriteLine();
            Console.WriteLine();

        }
        static void Main(string[] args)
        {
            // Initialization of variables at 0
            int numPlatforms = 0;
            int option = 0;

            // Prompt the user to enter the number of platforms
            Console.WriteLine("Enter the number of platforms to create");
            numPlatforms = Int32.Parse(Console.ReadLine());

            // Creates a new Station object with the specified number of platforms
            Station station = new Station("UFV Train Station", numPlatforms);

            // Loop to display the menu until the user chooses to exit
            while (option != 3)
            {
                PrintMenu();
                option = Int32.Parse(Console.ReadLine());

                if (option == 1)
                {
                    station.LoadTrainsFromFile();       // Calls the method to load trains from a CSV file
                }
                if (option == 2)
                {
                    station.StartSimulation();      // Calls the method to start the simulation
                }
                if (option <= 0 || option >= 4)
                {
                    Console.WriteLine("Error. Choose an option between 1 and 3");       // Displays an error message if the user enters an invalid option
                }
            }
            return;
        }
    }
}