namespace Tomato.ViewModels
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Mvvm;

    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(CountdownViewModel countdownViewModel)
        {
            Countdown = countdownViewModel;
            ExitCommand = new DelegateCommand(() => Process.GetCurrentProcess().Kill());
        }

        public ICommand ExitCommand { get; }

        public CountdownViewModel Countdown { get; }

        public CountdownViewModel Tracker { get; }
    }
}
