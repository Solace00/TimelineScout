using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibVLCSharp.Shared;
using LibVLCSharp.Avalonia;
using System;
using System.Windows.Input;

namespace TimeLineScout.ViewModels
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
            // Initialize LibVLC
            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            // Set the MediaPlayer to the VideoView
            videoView.MediaPlayer = _mediaPlayer;

            PlayCommand = new RelayCommand(OnPlay);
            PauseCommand = new RelayCommand(OnPause);
            StopCommand = new RelayCommand(OnStop);
        }

        private void OnPlay()
        {
            if (!string.IsNullOrEmpty(FilePath))
            {
                var media = new Media(_libVLC, new Uri(FilePath));
                _mediaPlayer.Play(media);
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