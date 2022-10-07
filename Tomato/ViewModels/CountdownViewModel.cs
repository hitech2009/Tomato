namespace Tomato.ViewModels
{
    using System;
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Mvvm;
    using Tomato.Services;

    public class CountdownViewModel : BindableBase
    {
        private readonly ICountdownService countdownService;
        private readonly ITrackingService trackingService;

        public CountdownViewModel(ITrackingService trackingService, ICountdownService countdownService)
        {
            this.trackingService = trackingService;

            this.countdownService = countdownService;
            this.countdownService.OnUpdate += CountdownService_OnUpdate;

            StartStopCommand = new DelegateCommand(StartStopHandler);
            PauseCommand = new DelegateCommand(PauseHandler);
            RewindCommand = new DelegateCommand(() => RewindHandler());
            SetLongRestInterval = new DelegateCommand<string>(SetLongRestIntervalHandler);

            SetNextInterval();
        }
        public ICommand StartStopCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand RewindCommand { get; }
        public ICommand SetLongRestInterval { get; }

        public TrackingInterval Current { get; private set; }

        public bool IsStarted { get; private set; }

        public int Count { get; private set; }

        private void StartStopHandler()
        {
            if (IsStarted)
            {
                countdownService.Stop();
                IsStarted = false;
                Current = trackingService.Current;
                return;
            }

            countdownService.Start(Current.Interval);
            IsStarted = true;
        }

        private void PauseHandler()
        {
            countdownService.Stop();
            IsStarted = false;
        }

        private void RewindHandler()
        {
            SetNextInterval();
            countdownService.Current = Current.Interval;
        }

        private void SetLongRestIntervalHandler(string value)
        {
            var interval = int.Parse(value);
            Current = trackingService.UpdatedLongRestInterval(interval);
            countdownService.Current = Current.Interval;
        }

        private void CountdownService_OnUpdate()
        {
            Current = Current.UpdateInterval(countdownService.Current);
            if (IsStarted && !countdownService.IsStarted)
            {
                SetNextInterval();
                countdownService.Start(Current.Interval);
            }
        }

        private void SetNextInterval()
        {
            Current = trackingService.GetNextInterval();
            Count = trackingService.Count;
        }
    }
}