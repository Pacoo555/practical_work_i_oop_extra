using System;

namespace practicalWorkI
{

    public enum TrainStatus
    {
    EnRoute,
    Waiting,
    Docking,
    Docked
    }

    public class Train
    {
        public string ID { get; set; }
        public TrainStatus Status { get; set; }
        public int ArrivalTime { get; set; }
        public string Type { get; set; }

        public Train(string id, int arrivalTime, string type)
        {
            ID = id;
            ArrivalTime = arrivalTime;
            Type = type;
            Status = TrainStatus.EnRoute;
        }
    }
}