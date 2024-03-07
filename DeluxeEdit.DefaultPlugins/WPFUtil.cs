using System.Windows.Controls;

namespace DeluxeEdit.DefaultPlugins
{

    public class WPFUtil
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
           var result=control.Items.IndexOf(header) > -1;
           return result;
        }
       }
    }