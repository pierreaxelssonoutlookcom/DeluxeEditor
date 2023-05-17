using DeluxeEdit.Model.Interface;
using DeluxeEdit.Model;
using System.Reflection;

namespace DeluxeEdit.Plugins
{
    public class PluginManager
    {
        private static Assembly? loadedAsm=null;


        public void ShowPluginManager()
        {
        }
        public void LoadPlugins(PluginSource source)
        {
            //done:could be multiple plugisn in the same, FILE
            loadedAsm = Assembly.LoadFile(source.Path);
            foreach (var item in source.Items)
            {
                var newItem = loadedAsm.CreateInstance(item.ClassName) as INamedActionPlugin;
                if (newItem != null)  source.Items.Add(newItem);
            }
        }
        public void LoadPlugins()
        {
        }
        public  void AddPlugin()
        {
        }
        public void RemovePlugin()
        {
        }


    }
}
