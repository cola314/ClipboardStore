using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIndowsFormStudy
{
    public static class ClipboardItemListExtension
    {
        public static bool HasItem(this List<ClipboardItem> items, ClipboardItem item)
        {
            foreach(ClipboardItem listItem in items)
            {
                if(listItem == item)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 특정 아이템을 리스트에서 삭제(성공 여부 반환)
        /// </summary>
        /// <param name="items"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool RemoveItem(this List<ClipboardItem> items, ClipboardItem item)
        {
            for(int i=0; i<items.Count; i++)
            {
                if(items[i] == item)
                {
                    items.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}
