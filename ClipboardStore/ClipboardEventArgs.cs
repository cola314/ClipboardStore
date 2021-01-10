using System;
using System.Windows.Forms;

namespace WIndowsFormStudy
{
    internal class ClipboardEventArgs : EventArgs
    {
        public ClipboardItem Item { get; set; }

        public ClipboardEventArgs(ClipboardItem item)
        {
            this.Item = item;
        }
    }
}