using System;
using System.IO;
using Plugin.SimpleAudioPlayer;

namespace CPAP
{
    abstract class AudioPlayer
    {
        protected ISimpleAudioPlayer _player;
        protected bool _isPaused;

        public MusicFile CurrentSong { get; set; }
        public double CurrentPosition
        {
            get { return _player.CurrentPosition; }
        }
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

        protected virtual void LoadAudioFile()
        {
            using (var audioStream = new FileStream(CurrentSong.Path, FileMode.Open))
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

        protected virtual void OnPlaybackEnded(object sender, EventArgs args)
        {
            Stop();
        }
    }
}
