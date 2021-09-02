using System;
using System.Collections.Generic;
using System.Text;

namespace EmilWallin_Assignment7.Events
{
    public class VideoPlayerEvent : EventArgs
    {
        //EventArgs used to open/close the video window
        private bool isVideo;

        public VideoPlayerEvent(bool isVideo)
        {
            this.isVideo = isVideo;
        }

        public bool IsVideo
        {
            get { return isVideo; }
        }
    }
}
