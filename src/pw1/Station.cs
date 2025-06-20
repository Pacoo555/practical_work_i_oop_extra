using System;

namespace practicalWorkI
{
    public class Station
    {
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

        public void DisplayStatus()
        {
            Console.WriteLine("----- TRAIN STATUS -----");

            foreach (var train in trains)
            {
                Console.WriteLine($"Train {train.ID} - Status: {train.Status}, Arrival: {train.ArrivalTime} min");
            }
            Console.WriteLine("");

            Console.WriteLine("----- PLATFORM STATUS -----");

            foreach (var platform in platforms)
            {
                if (platform.Status == PlatformStatus.Free)
                {
                    Console.WriteLine($"Platform {platform.ID}: Free");
                }
                else
                {
                    Console.WriteLine($"Platform {platform.ID}: Occupied by {platform.CurrentTrain.ID}, {platform.DockingTime + 1} ticks remaining");
                }
            }
            Console.WriteLine("");
        }

        public void LoadTrainsFromFile()
        {
            string filePath = "../../../../../files/";

            string filename = "";

            Console.WriteLine("Enter the name of the CSV file");
            filename = Console.ReadLine();

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
                            isFirstLine = false;
                            continue;
                        }

                        string[] values = line.Split(separator);

                        string id = values[0];
                        int arrivalTime = int.Parse(values[1]);
                        string type = values[2];

                        if (type == "Passenger")
                        {
                            int numberOfCarriages = int.Parse(values[3]);
                            int capacity = int.Parse(values[4]);

                            PassengerTrain passengerTrain = new PassengerTrain(id, arrivalTime, type, numberOfCarriages, capacity);
                            trains.Add(passengerTrain);
                        }
                        else if (type == "Freight")
                        {
                            int maxWeight = int.Parse(values[3]);
                            string freightType = values[4];

                            FreightTrain freightTrain = new FreightTrain(id, arrivalTime, maxWeight, freightType);
                            trains.Add(freightTrain);
                        }
                    }
                }

                Console.WriteLine($"Train data loaded successfully. " + trains.Count + " trains loaded.");
            }
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
                if (train.Status == TrainStatus.EnRoute || train.Status == TrainStatus.Waiting)
                {
                    train.ArrivalTime -= 15;

                    if (train.ArrivalTime <= 0)
                    {
                        train.ArrivalTime = 0;

                        bool platformAssigned = false;

                        foreach (var platform in platforms)
                        {
                            if (platform.Status == PlatformStatus.Free)
                            {
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
                            train.Status = TrainStatus.Waiting;
                        }
                    }
                }
            }

            foreach (var platform in platforms)
            {
                if (platform.Status == PlatformStatus.Occupied && platform.CurrentTrain != null)
                {
                    if (platform.CurrentTrain.Status == TrainStatus.Docking)
                    {
                        if (platform.DockingTime <= 0)
                        {
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

            while (!allDocked)
            {
                foreach (Train train in trains)
                {
                    if (train.Status != TrainStatus.Docked)
                    {
                        allDocked = false;
                        break;
                    }
                    else
                    {
                        allDocked = true;
                    }
                }
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