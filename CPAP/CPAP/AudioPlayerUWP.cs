﻿using System;
using System.IO;

namespace CPAP
{
    class AudioPlayerUWP : AudioPlayer
    {
        FileStream _audioStream;

        protected override void LoadAudioFile()
        {
            _audioStream?.Dispose();
            _audioStream = new FileStream(CurrentSong.FileHandle, FileAccess.Read);
            _player.Load(_audioStream);
        }
    }
}
