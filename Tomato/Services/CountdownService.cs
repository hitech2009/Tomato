namespace Tomato.Services
{
    using System;
    using System.Timers;

    public interface ICountdownService
    {
        event Action OnUpdate;
        bool IsStarted { get; }
        TimeSpan Current { get; set; }
        void Start(TimeSpan interval);
        void Stop();
    }

    internal class CountdownService : ICountdownService
    {
        private Timer timer = new Timer(1000);

        public CountdownService()
        {
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Current = Current.Subtract(TimeSpan.FromSeconds(1));
            if (Current.Minutes == 0 && Current.Seconds == 0)
            {
                Stop();
            }

            OnUpdate?.Invoke();
        }

        public event Action OnUpdate;

        public bool IsStarted { get; private set; }

        public TimeSpan Current { get; set; }

        public void Start(TimeSpan interval)
        {
            Current = interval;
            IsStarted = true;
            timer.Start();
        }

        public void Stop()
        {
            IsStarted = false;
            timer.Stop();
        }
    }
}
