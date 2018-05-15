using System;
using System.Collections.Generic;
using System.Text;
using Plugin.SimpleAudioPlayer;

namespace CPAP
{
    class AudioPlayer
    {
        ISimpleAudioPlayer _player;

        public MusicFile CurrentSong { get; set; }
        public double CurrentPosition { get; }
        public bool IsPlaying
        {
            get { return _player.IsPlaying; }
        }

        public AudioPlayer()
        {
            _player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        }

        public void Play()
        {
            if (!_player.IsPlaying)
            {
                using (System.IO.FileStream audioStream = new System.IO.FileStream(CurrentSong.Path, System.IO.FileMode.Open))
                {
                    _player.Load(audioStream);
                }
                _player.Play();
            }
            else
            {
                _player.Pause();
            }
        }

        public void Stop()
        {
            _player.Stop();
        }

        public string UpdatePlaybackTimer()
        {
            return FormatTime(_player.CurrentPosition);
        }

        private string FormatTime(double time)
        {
            string timeString;

            if (time >= 60)
            {
                int minutes = (int)time / 60;
                double seconds = time - (60 * minutes);
                timeString = minutes.ToString() + ":" + seconds.ToString("00");
            }
            else
            {
                timeString = time.ToString("0:00");
            }

            return timeString;
        }
    }
}
