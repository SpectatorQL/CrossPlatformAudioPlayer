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
            _audioPlayer = new AudioPlayer();
            SetUpEvents();
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
            playButton.IsEnabled = true;
            previousButton.IsEnabled = true;
            nextButton.IsEnabled = true;
            stopButton.IsEnabled = true;
        }

        public void Reset()
        {
            _audioPlayer.Stop();
            currentSongName.Text = null;
            currentSongTime.Text = "-:--";
        }

        private void SetUpEvents()
        {
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                currentSongTime.Text = _audioPlayer.UpdatePlaybackTimer();
                return true;
            });
        }

        private void playButton_Clicked(object sender, EventArgs args)
        {
            _audioPlayer.Play();
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
            _audioPlayer.Play();
        }

        private void nextButton_Clicked(object sender, EventArgs args)
        {
            _audioPlayer.Stop();
            _songList.NextTrack();
            _audioPlayer.Play();
        }

        private async void songButton_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(_songList);
        }
    }
}
