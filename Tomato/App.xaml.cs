namespace Tomato
{
    using System.Windows;
    using Prism.Ioc;
    using Tomato.Services;
    using Tomato.ViewModels;
    using Tomato.Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<MainWindowViewModel>();
            containerRegistry.Register<CountdownViewModel>();
            containerRegistry.Register<ICountdownService, CountdownService>();
            containerRegistry.Register<ITrackingService, TrackingService>();
        }
    }
}