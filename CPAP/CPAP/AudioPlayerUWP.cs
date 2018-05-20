using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CPAP
{
    class AudioPlayerUWP : AudioPlayer
    {
        FileStream _audioStream;

        public AudioPlayerUWP() : base() { }

        public override void Play()
        {
            if (!_player.IsPlaying)
            {
                if (_audioStream != null)
                    _audioStream.Close();

                _audioStream = new FileStream(CurrentSong.FileHandle, FileAccess.Read);
                _player.Load(_audioStream);
                _player.Play();
            }
            else
            {
                _player.Pause();
            }
        }

        public override void Stop()
        {
            base.Stop();
            _audioStream.Close();
        }
    }
}
