using System;
using Xamarin.Forms;

namespace CPAP
{
	public partial class MainPage : ContentPage
	{
        SongListPage _songList;
        AudioPlayer _audioPlayer;

        public MusicFile CurrentSong { get; set; }

        public MainPage()
        {
            InitializeComponent();
            _songList = new SongListPage(this);
            if (Device.RuntimePlatform == Device.UWP)
                _audioPlayer = new AudioPlayerUWP();
            else
                _audioPlayer = new AudioPlayer();
        }

        public void UpdateData()
        {
            EnableButtons();
            _audioPlayer.Stop();
            currentSongName.Text = CurrentSong.Name;
            currentSongTime.Text = "0:00";
            _audioPlayer.CurrentSong = CurrentSong;
        }

        private void EnableButtons()
        {
            if (playButton.IsEnabled)
                return;

            playButton.IsEnabled = true;
            previousButton.IsEnabled = true;
            nextButton.IsEnabled = true;
            stopButton.IsEnabled = true;
        }

        public void Play()
        {
            _audioPlayer.Play();
            StartPlaybackTimer();
        }

        public void Reset()
        {
            DisableButtons();
            _audioPlayer.Stop();
            currentSongName.Text = null;
            currentSongTime.Text = "-:--";
        }
        
        private void DisableButtons()
        {
            if (!playButton.IsEnabled)
                return;

            playButton.IsEnabled = false;
            previousButton.IsEnabled = false;
            nextButton.IsEnabled = false;
            stopButton.IsEnabled = false;
        }

        private void StartPlaybackTimer()
        {
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (_audioPlayer.IsPlaying)
                {
                    currentSongTime.Text = Time.Format(_audioPlayer.CurrentPosition);
                    return true;
                }
                else
                    return false;
            });
        }

        private void playButton_Clicked(object sender, EventArgs args)
        {
            Play();
        }

        private void stopButton_Clicked(object sender, EventArgs args)
        {
            _audioPlayer.Stop();
            currentSongTime.Text = "0:00";
        }

        private void previousButton_Clicked(object sender, EventArgs args)
        {
            _audioPlayer.Stop();
            _songList.ChangeSong(Song.Previous);
        }

        private void nextButton_Clicked(object sender, EventArgs args)
        {
            _audioPlayer.Stop();
            _songList.ChangeSong(Song.Next);
        }

        private async void songButton_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(_songList);
        }

        private void volumeSlider_ValueChanged(object sender, EventArgs args)
        {
            _audioPlayer.Volume = volumeSlider.Value;
        }
    }
}
