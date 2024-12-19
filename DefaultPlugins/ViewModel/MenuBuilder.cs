using Model;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using DefaultPlugins;
using Shared;
using System.Windows.Controls;
using Extensions.Util;
using System.Reflection.Metadata;
using System;

namespace ViewModel
{
    public class MenuBuilder
    {

        public static List<CustomMenu> MainMenu = new MenuBuilder().BuildMenu();
        public List<CustomMenu> BuildMenu()
        {
            var plugins = AllPlugins.InvokePlugins(PluginManager.GetPluginsLocal());
            var result = GetMenuHeaders(plugins);

            foreach (var item in result)
            {
                item.MenuItems.AddRange(GetMenuItemsForHeader(item.Header, plugins));
                if (item.Header== "View") item.MenuItems.AddRange(GetMenuItemsForFileTypes());

            }



            return result;
        }


        public List<CustomMenu> GetMenuHeaders(IEnumerable<INamedActionPlugin> plugins)
        {

            var result = plugins.Where(p => p.Configuration.ShowInMenu.HasContent() && p.Configuration.ShowInMenuItem.HasContent())
                .Select(p => p.Configuration.ShowInMenu).Distinct()
            .Select(p => new CustomMenu { Header = p }).ToList();
            result.Add( new CustomMenu { Header = "View" });

            return result;
        }

        public List<CustomMenuItem> GetMenuItemsForHeader(string header, IEnumerable<INamedActionPlugin> plugins)
        {
            var withMenu = plugins.Where(p => p.Configuration.ShowInMenu.HasContent() && p.Configuration.ShowInMenuItem.HasContent()).ToList();
            var myItems = withMenu.Where(p => p.Configuration.ShowInMenu == header).ToList();
            var test = myItems.Select(p => p.Configuration.ShowInMenuItem).ToList();
            var result = myItems
                .Select(p => new CustomMenuItem { Title = $"{p.Configuration.ShowInMenuItem} ({p.Configuration.KeyCommand})", Plugin = p })
                .ToList();

            return result;
        }
       public List<CustomMenuItem> GetMenuItemsForFileTypes()
        {

            var result = FileTypeLoader.AllFileTypes.Select(p => 
           new CustomMenuItem { Title = p.ToString()} ).ToList(); 
            return result;
        }


        public void ShowMenu(Menu mainMenu, List<CustomMenu> customMenus)
        {
            if (mainMenu == null) throw new ArgumentNullException();

            foreach (var item in customMenus)
            {
                int? index = null;
                if (mainMenu != null)
                    index = -WPFUtil.IndexOfText(mainMenu.Items, item.Header);

                int intindex = int.MinValue;
                if (index == null && mainMenu != null)
                {
                    index = mainMenu.Items.Add(new MenuItem { Header = item.Header });
                    intindex = index.Value;
                }
                else if (index.HasValue)
                    intindex = index.Value;



                foreach (var menuItem in item.MenuItems)
                {
                    MenuItem? newExistMenuItem = mainMenu != null && intindex <= mainMenu.Items.Count && mainMenu.Items[intindex] is MenuItem ? mainMenu.Items[intindex] as MenuItem : new MenuItem();
                    var newItem = new MenuItem { Header = menuItem.Title };
                    if (newExistMenuItem! != null) newExistMenuItem.Items.Add(newItem);

                }

            }




            


        }

    }
}
