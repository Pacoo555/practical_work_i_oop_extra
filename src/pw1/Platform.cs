using System;

namespace practicalWorkI
{
    public enum PlatformStatus
    {
        // Enum to represent the status of the platform
        Free,
        Occupied
    }
    public class Platform
    {
        // Attributes
        private string id;
        protected PlatformStatus status;
        private Train currentTrain;
        private int dockingTime;

        // Properties
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public PlatformStatus Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public Train CurrentTrain
        {
            get { return this.currentTrain; }
            set { this.currentTrain = value; }
        }

        public int DockingTime
        {
            get { return this.dockingTime; }
            set { this.dockingTime = value; }
        }

        // Constructor
        public Platform(string id)
        {
            this.id = id;
            this.dockingTime = 2;
            this.status = PlatformStatus.Free;
            this.currentTrain = null;
        }
    }
}