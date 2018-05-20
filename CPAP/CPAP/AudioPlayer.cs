using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Plugin.SimpleAudioPlayer;

namespace CPAP
{
    class AudioPlayer
    {
        protected ISimpleAudioPlayer _player;

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

        public virtual void Play()
        {
            if (!_player.IsPlaying)
            {
                using (FileStream audioStream = new FileStream(CurrentSong.Path, FileMode.Open))
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

        public virtual void Stop()
        {
            _player.Stop();
        }

        public string UpdatePlaybackTimer()
        {
            return FormatTime(_player.CurrentPosition);
        }

        protected string FormatTime(double time)
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
