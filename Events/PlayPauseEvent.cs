using System;
using System.Collections.Generic;
using System.Text;

namespace EmilWallin_Assignment7.Events
{
    //Toggle play and pause
    public class PlayPauseEvent : EventArgs
    {
        private bool isPlaying;

        public PlayPauseEvent(bool isPlaying)
        {
            this.isPlaying = isPlaying;
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
        }
    }
}
