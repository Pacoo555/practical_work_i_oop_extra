using System;

namespace practicalWorkI
{
    // Enum to represent the status of the train
    public enum TrainStatus
    {
        EnRoute,
        Waiting,
        Docking,
        Docked
    }

    public class Train
    {
        // Attributes
        private string id;
        protected TrainStatus status;
        private int arrivalTime;
        private string type;

        // Properties
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public TrainStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public int ArrivalTime
        {
            get { return arrivalTime; }
            set { arrivalTime = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        // Constructor
        public Train(string id, int arrivalTime, string type)
        {
            this.id = id;
            this.arrivalTime = arrivalTime;
            this.type = type;
            this.status = TrainStatus.EnRoute;
        }
    }
}