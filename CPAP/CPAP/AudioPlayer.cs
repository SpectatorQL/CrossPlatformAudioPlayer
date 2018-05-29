using System;
using System.IO;
using Plugin.SimpleAudioPlayer;

namespace CPAP
{
    class AudioPlayer
    {
        protected ISimpleAudioPlayer _player;
        protected bool _isPaused;

        public MusicFile CurrentSong { get; set; }
        public double CurrentPosition { get; }
        public double Volume
        {
            set { _player.Volume = value; }
        }
        public bool IsPlaying
        {
            get { return _player.IsPlaying; }
        }

        public AudioPlayer()
        {
            _player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            _player.PlaybackEnded += OnPlaybackEnded;
        }

        public virtual void Play()
        {
            if (!IsPlaying)
            {
                if (!_isPaused)
                    LoadAudioFile();
                PlayFile();
            }
            else
                Pause();
        }

        public virtual void Stop()
        {
            _player.Stop();
            _isPaused = false;
        }

        public string UpdatePlaybackTimer()
        {
            return FormatTime(_player.CurrentPosition);
        }

        protected virtual void LoadAudioFile()
        {
            using (FileStream audioStream = new FileStream(CurrentSong.Path, FileMode.Open))
            {
                _player.Load(audioStream);
            }
        }

        protected void Pause()
        {
            _player.Pause();
            _isPaused = true;
        }

        protected void PlayFile()
        {
            _player.Play();
            _isPaused = false;
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

        protected void OnPlaybackEnded(object sender, EventArgs args)
        {
            Stop();
        }
    }
}
