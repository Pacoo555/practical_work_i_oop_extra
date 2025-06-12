using System;

namespace practicalWorkI
{
    public class Station
    {
        private List<Platform> platforms;
        private List<Train> trains;

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

        public void DisplayStatus()
        {
            Console.WriteLine("----- TRAIN STATUS -----");

            foreach (var train in trains)
            {
                Console.WriteLine($"Train {train.ID} - Status: {train.Status}, Arrival: {train.ArrivalTime} min");
            }

            Console.WriteLine("----- PLATFORM STATUS -----");

            foreach (var platform in platforms)
            {
                if (platform.Status == PlatformStatus.Free)
                {
                    Console.WriteLine($"Platform {platform.ID}: Free");
                }
                else
                {
                    Console.WriteLine($"Platform {platform.ID}: Occupied by {platform.CurrentTrain.ID}, {platform.DockingTime} ticks remaining");
                }
            }
        }

        public void LoadTrainsFromFile()
        {
            string filePath = "../files/Trains.csv";

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
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

                Console.WriteLine("Train data loaded successfully.");
            }
            catch (FileNotFoundException f)
            {
                Console.WriteLine($"{f}; File does not exist.");
            }
            catch (DirectoryNotFoundException d)
            {
                Console.WriteLine($"{d}; Directory does not exist.");
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
                        platform.DockingTime--;

                        if (platform.DockingTime <= 0)
                        {
                            platform.CurrentTrain.Status = TrainStatus.Docked;
                            platform.CurrentTrain = null;
                            platform.Status = PlatformStatus.Free;
                        }
                    }
                }
            }
        }


    }
}