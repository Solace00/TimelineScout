using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibVLCSharp.Shared;
using LibVLCSharp.Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using System.Windows.Input;

namespace TimelineScout.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public ICommand PlayCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand StopCommand { get; }

        [ObservableProperty]
        private string? _filePath; // Stores the selected video file path

        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;

        public MainWindowViewModel(VideoView videoView)
        {
            if (videoView == null)
            {
                throw new ArgumentNullException(nameof(videoView), "VideoView cannot be null.");
            }

            // Initialize LibVLC only when needed
            PlayCommand = new RelayCommand(OnPlay);
            PauseCommand = new RelayCommand(OnPause);
            StopCommand = new RelayCommand(OnStop);

            // Set the VideoView
            videoView.MediaPlayer = _mediaPlayer;
        }

        private void InitializeLibVLC()
        {
            if (_libVLC == null)
            {
                Core.Initialize();
                _libVLC = new LibVLC();
                _mediaPlayer = new MediaPlayer(_libVLC);
            }
        }

        private async void OnPlay()
        {
            InitializeLibVLC();
            // Get the main window (or inject it)
            var window = App.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
            if (window?.MainWindow == null)
            {
                Console.WriteLine("Main window is not available.");
                return;
            }

            // Use StorageProvider to open a file dialog
            var filePickerOptions = new FilePickerOpenOptions
            {
                Title = "Select a video file",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("Video Files")
                    {
                        Patterns = new[] { "*.mp4", "*.avi", "*.mkv" }
                    }
                }
            };

            var files = await window.MainWindow.StorageProvider.OpenFilePickerAsync(filePickerOptions);

            if (files != null && files.Count > 0)
            {
                FilePath = files[0].Path.LocalPath; // Store the selected file path
                Console.WriteLine($"Playing video: {FilePath}");

                // Play the selected video
                var media = new Media(_libVLC, new Uri(FilePath));
                _mediaPlayer.Play(media);
            }
            else
            {
                Console.WriteLine("No file selected.");
            }
        }

        private void OnPause()
        {
            _mediaPlayer.Pause();
        }

        private void OnStop()
        {
            _mediaPlayer.Stop();
        }
    }
}