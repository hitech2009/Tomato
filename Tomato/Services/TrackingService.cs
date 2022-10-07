using System;

namespace Tomato.Services
{
    public interface ITrackingService
    {
        TrackingInterval Current { get; }
        int Count { get; }
        TrackingInterval GetNextInterval();
        TrackingInterval UpdatedLongRestInterval(int interval);
    }

    internal class TrackingService : ITrackingService
    {
        public int Count { get; private set; } = 0;
        public TrackingInterval Current { get; private set; } = TrackingInterval.WorkingTrack;

        public TrackingInterval GetNextInterval()
        {
            if (Count == 0 || !Current.IsWorkingTrack)
            {
                Count++;
                Current = TrackingInterval.WorkingTrack;
            }
            else if (Current.IsWorkingTrack && Count % 4 == 0)
            {
                Current = TrackingInterval.LongRestTrack;
            }
            else
            {
                Current = TrackingInterval.RestTrack;
            }

            return Current;
        }

        public TrackingInterval UpdatedLongRestInterval(int value)
        {
            var interval = TimeSpan.FromMinutes(value);
            TrackingInterval.LongRestTrack = TrackingInterval.LongRestTrack.UpdateInterval(interval);
            Current = TrackingInterval.LongRestTrack;
            return Current;
        }
    }
}