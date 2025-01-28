using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Avalonia;
using System;
using TimelineScout.ViewModels;

namespace TimelineScout.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            // Find the VideoView control and pass it to the ViewModel
            var videoView = this.FindControl<VideoView>("VideoPlayer");
            if (videoView == null)
            {
                throw new InvalidOperationException("VideoPlayer control not found.");
            }

            DataContext = new MainWindowViewModel(videoView);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}