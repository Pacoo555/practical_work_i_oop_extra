using System;

namespace practicalWorkI
{
    public class PassengerTrain : Train
    {
        //Attributes
        private int numberOfCarriages;
        private int capacity;

        //Properties
        public int NumberOfCarriages
        {
            get { return numberOfCarriages; }
            set { numberOfCarriages = value; }
        }

        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        //Constructor
        public PassengerTrain(string id, int arrivalTime, string type, int numberOfCarriages, int capacity) : base(id, arrivalTime, "passenger")
        {
            this.numberOfCarriages = numberOfCarriages;
            this.capacity = capacity;
        }
    }
}