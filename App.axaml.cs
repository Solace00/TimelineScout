using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Avalonia;
using System;
using TimelineScout.ViewModels;
using TimelineScout.Views;

namespace TimelineScout
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

                // Find the VideoView control and pass it to the ViewModel
                var videoView = mainWindow.FindControl<VideoView>("VideoPlayer");
                if (videoView == null)
                {
                    throw new InvalidOperationException("VideoPlayer control not found.");
                }

                mainWindow.DataContext = new MainWindowViewModel(videoView); // Set the DataContext
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}