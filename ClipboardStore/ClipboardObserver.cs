using System;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WIndowsFormStudy
{
    internal class ClipboardObserver : IDisposable
    {
        public delegate void ClipboardEventHandler(object sender, ClipboardEventArgs e);

        public event ClipboardEventHandler ItemAdded;

        private int sleepTime = 100;

        private bool running_;

        private Timer timer;

        private ClipboardItem lastItem;

        public ClipboardObserver()
        {
        }

        public void Dispose()
        {
            running_ = false;
            if(timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
        }

        internal void Run()
        {
            if(!running_)
            {
                running_ = true;
                timer = new Timer();
                timer.Interval = sleepTime;
                timer.Tick += (s, e) =>
                {
                    ClipboardItem item = ClipboardItem.Create(Clipboard.GetDataObject(), Clipboard.GetText(), Clipboard.GetImage());

                    if (item != null && item != lastItem)
                    {
                        lastItem = item;
                        ItemAdded(this, new ClipboardEventArgs(item));
                    }
                };
                timer.Enabled = true;
            }
        }
    }
}