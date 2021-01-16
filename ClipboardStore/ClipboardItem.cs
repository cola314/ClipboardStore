using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WIndowsFormStudy
{
    public class ClipboardItem : ICloneable
    {
        public static List<string> SupportedFormat { get; private set; } = new List<string>();
        public string DisplayText { get; private set; }
        public Image DisplayImage { get; private set; }
        public DataObject Data { get; private set; }

        static ClipboardItem()
        {
            SupportedFormat.Add(typeof(string).ToString());
            SupportedFormat.Add(typeof(Bitmap).ToString());
        }
        
        //use Create method to create instance
        private ClipboardItem()
        {
        }

        public static ClipboardItem Create(IDataObject obj, string displayText = "", Image displayImage = null)
        {
            if(obj?.GetFormats().Any() ?? false)
            {
                foreach(string format in SupportedFormat)
                {
                    if(obj.GetFormats().Contains(format))
                    {
                        var data = new DataObject();
                        foreach(string dataFormat in SupportedFormat)
                        {
                            data.SetData(dataFormat, obj.GetData(dataFormat));
                        }
                        return new ClipboardItem()
                        {
                            Data = data,
                            DisplayText = displayText,
                            DisplayImage = displayImage
                        };
                    }
                }
            }
            return null;
        }

        public object Clone()
        {
            return new ClipboardItem()
            {
                Data = this.Data,
                DisplayText = this.DisplayText,
                DisplayImage = this.DisplayImage
            };
        }

        public static bool operator ==(ClipboardItem a, ClipboardItem b)
        {
            return a?.DisplayText == b?.DisplayText &&
                a?.DisplayImage?.Size == b?.DisplayImage?.Size;
        }

        public static bool operator !=(ClipboardItem a, ClipboardItem b)
        {
            return !(a == b);
        }
    }
}
