using Model.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extensions ;
using DefaultPlugins;
using Shared;
using System.Windows.Controls;

namespace DeluxeEdit.DefaultPlugins.ViewModel
{
    public class MenuBuilder
    {

        public List<CustomMenu> BuildMenu()
        {
            var plugins = AllPlugins.InvokePlugins(PluginManager.GetPluginsLocal());
           var result = GetMenuHeaders(plugins);

            foreach (var item in result) 
                item.MenuItems.AddRange(
                    GetMenuItemsForHeader(item.Header, plugins));



            return result;
        }

        public List<CustomMenu> GetMenuHeaders(IEnumerable<INamedActionPlugin> plugins)
        {
            
            var result = plugins.Where(p => p.Configuration.ShowInMenu.HasContent() && p.Configuration.ShowInMenuItem.HasContent())
                .Select(p => p.Configuration.ShowInMenu).Distinct()
            .Select(p => new CustomMenu { Header = p }).ToList();

            return result;
        }

        public List<CustomMenuItem> GetMenuItemsForHeader(string header, IEnumerable<INamedActionPlugin> plugins)
        {
            if (header == null) throw new ArgumentNullException("header");
            var withMenu = plugins.Where(p => p.Configuration.ShowInMenu.HasContent() && p.Configuration.ShowInMenuItem.HasContent());
            var myItemss = withMenu.Where(p => p.Configuration.ShowInMenu == header);
            var result = myItemss.Select(p =>

                new CustomMenuItem { Title = $" ({p.Configuration.ShowInMenuItem} {p.Configuration.KeyCommand.Keys.ToString()}) ", Plugin = p })
                .ToList();

            return result;
        }


        public void ShowMenu(Menu mainMenu, List<CustomMenu> customMenus)
        {

            foreach (var item in customMenus)
            {

                var index =-WPFUtil.IndexOfText(     mainMenu.Items, item.Header);
                if (index==  null )
                {
                    index = mainMenu.Items.Add(new MenuItem { Header = item.Header });
                }

                foreach (var menuItem in item.MenuItems)
                {
                    MenuItem newExistMenuItem =  mainMenu.Items[index.Value] as MenuItem;
                    var newItem = new MenuItem { Header = menuItem.Title };
                    newExistMenuItem.Items.Add(newItem);

                }

            }

       
        
        
        
        
        
        }

    }
}
