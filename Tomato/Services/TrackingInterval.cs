namespace Tomato.Services
{
    using System;

    public struct TrackingInterval
    {
        private const string Zero = "00";

        private const int WorkingIntervalMins = 25;
        private const int RestIntervalMins = 5;
        private const int LongRestIntervalMins = 15;

        public static TrackingInterval EmptyTrack = new TrackingInterval(false, false, TimeSpan.FromMinutes(0));
        public static TrackingInterval WorkingTrack = new TrackingInterval(false, false, TimeSpan.FromMinutes(WorkingIntervalMins));
        public static TrackingInterval RestTrack = new TrackingInterval(true, false, TimeSpan.FromMinutes(RestIntervalMins));
        public static TrackingInterval LongRestTrack = new TrackingInterval(false, true, TimeSpan.FromMinutes(LongRestIntervalMins));

        private TrackingInterval(bool isRest, bool isLongRest, TimeSpan interval)
        {
            IsRestTrack = isRest;
            IsLongRestTrack = isLongRest;
            IsWorkingTrack = !(IsRestTrack || IsLongRestTrack); ;
            Interval = interval;
            Minutes = Zero;
            Seconds = Zero;

            UpdateInterval(interval);
        }

        public bool IsWorkingTrack { get; }

        public bool IsRestTrack { get; }

        public bool IsLongRestTrack { get; }

        public TimeSpan Interval { get; private set; }

        public string Minutes { get; private set; }
        
        public string Seconds { get; private set; }

        public TrackingInterval UpdateInterval(TimeSpan interval)
        {
            Interval = interval;
            Minutes = FormatValue(Interval.Minutes);
            Seconds = FormatValue(Interval.Seconds);
            return this;
        }

        private static string FormatValue(double value)
        {
            var result = value.ToString();
            if (result.Length < 2)
            {
                result = "0" + result;
            }

            return result;
        }
    }
}
