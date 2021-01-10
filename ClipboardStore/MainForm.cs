using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WIndowsFormStudy
{
    public partial class MainForm : Form
    {
        private KeyboardHook keyHook;

        private Action ActivateForm;

        private ClipboardObserver co;

        private List<ClipboardItem> clipboardBuffer = new List<ClipboardItem>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitContextMenu();
            InitButton();

            this.Deactivate += (s, e_) => this.WindowState = FormWindowState.Minimized;
            
            keyHook = new KeyboardHook();
            ActivateForm = () =>
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    this.WindowState = FormWindowState.Normal;
                }));
            };
            keyHook.KeyPressed += ActivateForm;

            FormClosing += (s, e_) => keyHook.Dispose();
            
            co = new ClipboardObserver();
            FormClosing += (s, e_) => co.Dispose(); 
            co.ItemAdded += ClipboardItemAdded;
            co.Run();
        }

        private void InitButton()
        {
            button1.Click += (s, e_) => SetClipboard(0);
            button2.Click += (s, e_) => SetClipboard(1);
            button3.Click += (s, e_) => SetClipboard(2);
            button4.Click += (s, e_) => SetClipboard(3);
            button5.Click += (s, e_) => SetClipboard(4);
        }

        private void SetClipboard(int idx)
        {
            if (clipboardBuffer.Count > idx)
            {
                try
                {
                    Clipboard.SetDataObject((clipboardBuffer[idx].Clone() as ClipboardItem).Data, true);
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            this.WindowState = FormWindowState.Minimized;
        }

        private void ClipboardItemAdded(object sender, ClipboardEventArgs e)
        {
            clipboardBuffer.Insert(0, e.Item);

            //5개까지 클립보드를 저장함
            if (clipboardBuffer.Count > 5)
            {
               clipboardBuffer.RemoveAt(clipboardBuffer.Count - 1);
            }

            RefreshButtons();
        }
        
        private void RefreshButtons()
        {
            RefreshClipboardItemButton(button1, 0);
            RefreshClipboardItemButton(button2, 1);
            RefreshClipboardItemButton(button3, 2);
            RefreshClipboardItemButton(button4, 3);
            RefreshClipboardItemButton(button5, 4);
        }

        private void RefreshClipboardItemButton(Button button, int idx)
        {
            if(clipboardBuffer.Count > idx)
            {
                button.Text = clipboardBuffer[idx].DisplayText;
                button.Image = clipboardBuffer[idx].DisplayImage;
            }
            else
            {
                button.Text = "No Item";
                button.Image = null;
            }
        }

        private void InitContextMenu()
        {
            ContextMenu ctx = new ContextMenu();
            ctx.MenuItems.Add(new MenuItem("열기", (s, e) => this.WindowState = FormWindowState.Normal));
            ctx.MenuItems.Add("-");
            ctx.MenuItems.Add(new MenuItem("끝내기", (s, e) => this.Close()));
            FormClosing += (s, e) => ctx.Dispose(); 
            notifyIcon1.ContextMenu = ctx;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Escape:
                    this.WindowState = FormWindowState.Minimized;
                    break;

                case Keys.D1:
                    button1.PerformClick();
                    break;

                case Keys.D2:
                    button2.PerformClick();
                    break;

                case Keys.D3:
                    button3.PerformClick();
                    break;

                case Keys.D4:
                    button4.PerformClick();
                    break;

                case Keys.D5:
                    button5.PerformClick();
                    break;

                default:
                    break;
            }
        }
    }
}
