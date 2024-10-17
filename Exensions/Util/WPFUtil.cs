using System.Windows.Controls;

namespace Exensions.Util
{

    public  static class WPFUtil
    {
        public const nint Minus1 = -1;


        public static TabItem AddOrUpddateTab(string header, TabControl control, object contentControl)
        {
            TabItem? item = null;
            var index = IndexOfText(control.Items, header);
            if (index.HasValue)
            {
             
                
                item =control.Items[index.Value] is TabItem ? control.Items[index.Value] as TabItem : null;

                
            }
            else
            {
                item = new TabItem();
                control.Items.Add(item);

            }
            item.Header= header;
            item.Content= contentControl;
            return item;
        }


        public static bool TabÉxist(string header, TabControl control)
        {
            var result = IndexOfText(control.Items, header) != null;
                ;
            return result;
        }
        public static int? IndexOfText(ItemCollection  collection, string text)
        {
            int? result = null;

            for (int i = 0; i < collection.Count; i++)
            { 
                string header = "";

                if (collection[i] is HeaderedItemsControl)
                {
                    var obj = collection[i] is HeaderedItemsControl ? collection[i] as HeaderedItemsControl : null;


                    if (obj != null) header = obj.Header.ToString();
                }
                else if (collection[i] is TabItem)
                {
                    var t = collection[i] as TabItem;
                      if (t != null) header = t.Header.ToString();


                }

               if (header == text)  
               {
                   result = i;
                        ;
                        break;
                }
            }


            return result ;
        } 

    }
}