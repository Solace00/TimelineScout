using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TimelineScout.ViewModels;
using TimelineScout.Views;
using TimeLineScout.ViewModels;
using TimeLineScout.Views;

namespace TimeLineScout
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = new MainWindow(); // Create the MainWindow
                desktop.MainWindow = mainWindow; // Set it as the main window

                // Pass the MainWindow to the ViewModel
                mainWindow.DataContext = new MainWindowViewModel(mainWindow.VideoPlayer);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}