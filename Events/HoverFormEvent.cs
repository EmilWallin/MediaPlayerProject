using EmilWallin_Assignment7.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmilWallin_Assignment7.Events
{
    //Event for when a playlist is hovered over by the mouse
    public class HoverFormEvent : EventArgs
    {
        private bool isHovering;

        public HoverFormEvent(bool isHovering)
        {
            this.isHovering = isHovering;
        }

        public bool IsHovering
        {
            get { return isHovering; }
        }
    }
}
