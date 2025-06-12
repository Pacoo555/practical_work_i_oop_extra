using System;

namespace practicalWorkI
{
    public enum PlatformStatus
    {
        Free,
        Occupied
    }
    public class Platform
    {
        private string id;
        protected PlatformStatus status;
        private Train currentTrain;
        private int dockingTime;

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

        public Platform(string id)
        {
            this.id = id;
            this.dockingTime = 2;
            this.status = PlatformStatus.Free;
            this.currentTrain = null;
        }
    }
}