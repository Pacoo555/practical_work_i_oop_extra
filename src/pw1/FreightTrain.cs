using System;

namespace practicalWorkI
{
    public class FreightTrain : Train
    {
        //Attributes
        private int maxWeight;
        private string freightType;

        //Properties
        public int MaxWeight
        {
            get { return maxWeight; }
            set { maxWeight = value; }
        }

        public string FreightType
        {
            get { return freightType; }
            set { freightType = value; }
        }

        //Constructor
        public FreightTrain(string id, int arrivalTime, int maxWeight, string freightType) : base(id, arrivalTime, "freight")
        {
            this.maxWeight = maxWeight;
            this.freightType = freightType;
        }
    }
}