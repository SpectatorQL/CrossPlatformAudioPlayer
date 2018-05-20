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
            try
            {
                if (!_player.IsPlaying)
                {
                    if (_audioStream != null)
                        _audioStream.Dispose();

                    System.Diagnostics.Debug.WriteLine(CurrentSong.FileHandle.IsClosed.ToString());
                    _audioStream = new FileStream(CurrentSong.FileHandle, FileAccess.Read);
                    _player.Load(_audioStream);
                    _player.Play();
                }
                else
                {
                    _player.Pause();
                }
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
        }
    }
}
