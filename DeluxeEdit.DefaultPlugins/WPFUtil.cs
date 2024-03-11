using System.Windows.Controls;

namespace DeluxeEdit.DefaultPlugins
{

    public  static class WPFUtil
    {
        
        public static void AddOrUpddateTab(string header, TabControl control)
        {
            if (WPFUtil.TabÉxist(header, control) == false)
            {
                var item = new TabItem { Header = header };
                control.Items.Add(item);
            }
        }

        public static bool TabÉxist(string header, TabControl control)
        {
            var result = control.Items.IndexOfText(header) > -1;
            return result;
        }
        public static int IndexOfText(this ItemCollection  collection, string text)
        {
            int result = -1;
            for (int i = 0; i < collection.Count; i++)
            {
                var casted= collection[i] as HeaderedItemsControl;
                if (casted.Header==text  )
               {
                    result = i;
                    break;
                }
            }


            return result;
        }

    }
}