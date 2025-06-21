using System;

namespace practicalWorkI
{
    public class Station
    {
        // Attributes
        private List<Platform> platforms;
        private List<Train> trains;
        private string name;

        // Properties
        public List<Platform> Platforms
        {
            get { return this.platforms; }
            set { this.platforms = value; }
        }

        public List<Train> Trains
        {
            get { return this.trains; }
            set { this.trains = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public Station(string name, int numPlatforms)
        {
            this.name = name;
            this.platforms = new List<Platform>();
            this.trains = new List<Train>();

            for (int i = 0; i < numPlatforms; i++)
            {
                Platform platform = new Platform("P" + i);
                this.platforms.Add(platform);
            }            
        }

        // Methods

        // Displays the status of all trains and platforms
        public void DisplayStatus()
        {
            Console.WriteLine("----- TRAIN STATUS -----");

            foreach (var train in trains)
            {
                // Display the train ID, status, and arrival time
                Console.WriteLine($"Train {train.ID} - Status: {train.Status}, Arrival: {train.ArrivalTime} min");
            }
            Console.WriteLine("");

            Console.WriteLine("----- PLATFORM STATUS -----");

            foreach (var platform in platforms)
            {
                if (platform.Status == PlatformStatus.Free)     // Check if the platform is free
                {
                    Console.WriteLine($"Platform {platform.ID}: Free");
                }
                else
                {
                    // If the platform is occupied, display the train ID and remaining docking time 
                    Console.WriteLine($"Platform {platform.ID}: Occupied by {platform.CurrentTrain.ID}, {platform.DockingTime + 1} ticks remaining");
                }
            }
            Console.WriteLine("");
        }

        public void LoadTrainsFromFile()
        {
            string filePath = "../../../../../files/";      // Path to the CSV file

            string filename = "";       // Variable to store the filename, initialized as an empty string

            Console.WriteLine("Enter the name of the CSV file");
            filename = Console.ReadLine();      // Read the filename from user input

            try
            {
                using (StreamReader sr = new StreamReader(filePath + filename))
                {
                    string? line;
                    string separator = ",";
                    bool isFirstLine = true;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (isFirstLine)
                        {
                            isFirstLine = false;        // Avoids the first line of the file, which contains the header
                            continue;
                        }

                        string[] values = line.Split(separator);    // Splits the line into values using "," as the separator

                        // Initialize variables from the split values
                        string id = values[0];
                        int arrivalTime = int.Parse(values[1]);
                        string type = values[2];

                        if (type == "Passenger")
                        {
                            // Initialize additional variables for passenger trains
                            int numberOfCarriages = int.Parse(values[3]);
                            int capacity = int.Parse(values[4]);

                            // Create a new PassengerTrain object and add it to the trains list
                            PassengerTrain passengerTrain = new PassengerTrain(id, arrivalTime, type, numberOfCarriages, capacity);
                            trains.Add(passengerTrain);
                        }
                        else if (type == "Freight")
                        {
                            // Initialize additional variables for freight trains
                            int maxWeight = int.Parse(values[3]);
                            string freightType = values[4];

                            // Create a new FreightTrain object and add it to the trains list
                            FreightTrain freightTrain = new FreightTrain(id, arrivalTime, maxWeight, freightType);
                            trains.Add(freightTrain);
                        }
                    }
                }

                Console.WriteLine($"Train data loaded successfully. " + trains.Count + " trains loaded.");
            }
            // Handle multiple exceptions that may occur
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File does not exist. " + e.Message);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Formatting error " + ex.Message);
            }
            catch (FormatException f)
            {
                Console.WriteLine($"Format error " + f.Message);
            }
        }

        public void AdvanceTick()
        {
            foreach (var train in trains)
            {
                if (train.Status == TrainStatus.EnRoute || train.Status == TrainStatus.Waiting)         // If the train isn't docked or docking
                {
                    train.ArrivalTime -= 15;

                    if (train.ArrivalTime <= 0)     // If the train has arrived at the station
                    {
                        train.ArrivalTime = 0;

                        bool platformAssigned = false;      // Variable to check if a platform has been assigned

                        foreach (var platform in platforms)
                        {
                            if (platform.Status == PlatformStatus.Free)
                            {
                                // Assign the train to the free platform
                                platform.Status = PlatformStatus.Occupied;
                                platform.CurrentTrain = train;
                                platform.DockingTime = 2;
                                train.Status = TrainStatus.Docking;
                                platformAssigned = true;
                                break;
                            }
                        }

                        if (!platformAssigned)
                        {
                            // If no platform is free, set the train status to Waiting
                            train.Status = TrainStatus.Waiting;
                        }
                    }
                }
            }

            foreach (var platform in platforms)
            {
                // If the platform is occupied and has a train docking
                if (platform.Status == PlatformStatus.Occupied && platform.CurrentTrain != null)
                {
                    // If the train is docking, decrease the docking time
                    if (platform.CurrentTrain.Status == TrainStatus.Docking)
                    {
                        if (platform.DockingTime <= 0)
                        {
                            // If the docking time is over, set the train status to Docked
                            platform.CurrentTrain.Status = TrainStatus.Docked;
                            platform.CurrentTrain = null;
                            platform.Status = PlatformStatus.Free;
                        }
                        platform.DockingTime--;
                    }
                }
            }
        }

        public void StartSimulation()
        {
            bool allDocked = false;

            while (!allDocked)      // Check if all trains are docked in order to end the simulation or continue it
            {
                foreach (Train train in trains)
                {
                    if (train.Status != TrainStatus.Docked)
                    {
                        // If any train is not docked, set allDocked to false and continue the simulation
                        allDocked = false;
                        break;
                    }
                    else
                    {
                        allDocked = true;
                    }
                }

                // Execute the simulation methods
                AdvanceTick();
                Console.Clear();
                DisplayStatus();

                Console.WriteLine("Press enter to advance the simulation");
                Console.ReadLine();
                
            }
            Console.WriteLine("Simulation ended. All trains are docked.");
        }
    }
}