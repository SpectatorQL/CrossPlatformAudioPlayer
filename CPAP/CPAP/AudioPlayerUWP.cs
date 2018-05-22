using System;
using System.IO;

namespace CPAP
{
    class AudioPlayerUWP : AudioPlayer
    {
        FileStream _audioStream;
        bool _isPaused;

        public AudioPlayerUWP() : base() { }

        public override void Play()
        {
            try
            {
                if (!IsPlaying && !_isPaused)
                {
                    LoadAudioFile();
                    PlayFile();
                }
                else if(_isPaused)
                    PlayFile();
                else
                    Pause();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.InnerException + " " + e.Message + " " + e.GetType().ToString());
                throw e;
            }
        }

        public override void Stop()
        {
            base.Stop();
            _isPaused = false;
        }

        private void LoadAudioFile()
        {
            if (_audioStream != null)
                _audioStream.Dispose();

            _audioStream = new FileStream(CurrentSong.FileHandle, FileAccess.Read);
            _player.Load(_audioStream);
        }

        private void Pause()
        {
            _player.Pause();
            _isPaused = true;
        }

        private void PlayFile()
        {
            _player.Play();
            _isPaused = false;
        }
    }
}
