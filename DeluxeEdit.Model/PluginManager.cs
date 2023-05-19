using DeluxeEdit.Model.Interface;
using DeluxeEdit.Model;
using System.Reflection;
using System;

namespace DeluxeEdit.Model
{
    //todo:will now instead use nuget as plugins
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
            

            foreach (var t in loadedAsm.GetTypes())
            {
        
                var newItem= Activator.CreateInstance(t);
                var newItemCasted=newItem is INamedActionPlugin ? newItem as INamedActionPlugin : null; ;

                if (newItemCasted != null)
                    source.Items.Add(newItemCasted);
            }         }
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
