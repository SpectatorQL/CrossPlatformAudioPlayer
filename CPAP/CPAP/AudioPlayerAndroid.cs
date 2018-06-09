using System;
using System.Collections.Generic;
using System.Text;

namespace CPAP
{
    class AudioPlayerAndroid : AudioPlayer
    {
        // All this nonsense is needed because of the way Android MediaPlayer's states work.
        bool _canStop = true;

        public override void Play()
        {
            base.Play();
            _canStop = true;
        }

        public override void Stop()
        {
            if (_canStop)
                base.Stop();
        }

        protected override void OnPlaybackEnded(object sender, EventArgs args)
        {
            if (_canStop)
                Stop();
            _canStop = false;
        }
    }
}
