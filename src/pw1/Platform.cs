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
        public string PlatformID { get; set; }
        public PlatformStatus Status { get; set; }
        public Train CurrentTrain { get; set; }
        public int DockingTime { get; set; } = 2;

        public Platform(string id, int dockingTime)
        {
            PlatformID = id;
            dockingTime = DockingTime;
            Status = PlatformStatus.Free;
            CurrentTrain = null;
        }

    }
}