using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.SimpleAudioPlayer;

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
#if WINDOWS_UWP
            _audioPlayer = new AudioPlayerUWP();
#else
            _audioPlayer = new AudioPlayer();
#endif
        }

        public void UpdateData()
        {
            EnableButtons();
            _audioPlayer.Stop();
            currentSongName.Text = CurrentSong.Name;
            currentSongTime.Text = "0:00";
            _audioPlayer.CurrentSong = CurrentSong;
        }

        public void EnableButtons()
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
            _audioPlayer.Stop();
            currentSongName.Text = null;
            currentSongTime.Text = "-:--";
        }

        private void StartPlaybackTimer()
        {
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (_audioPlayer.IsPlaying)
                {
                    currentSongTime.Text = _audioPlayer.UpdatePlaybackTimer();
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
            _songList.PreviousTrack();
            Play();
        }

        private void nextButton_Clicked(object sender, EventArgs args)
        {
            _audioPlayer.Stop();
            _songList.NextTrack();
            Play();
        }

        private async void songButton_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(_songList);
        }
    }
}
