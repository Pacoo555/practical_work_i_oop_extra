using System;

namespace practicalWorkI
{
    public class Station
    {
        List<Platform> platforms = new List<Platform>();
        List<Train> trains = new List<Train>();
    
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
    }
}