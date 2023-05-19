using DeluxeEdit.Model.Interface;
using DeluxeEdit.Model;
using System.Reflection;
using System;

namespace DeluxeEdit.Model
{
    //todo:will now instead u se nuget as plugins
    public class PluginManager
    {
        private static Assembly? loadedAsm=null;


        public void ShowPluginManager()
        {
        }
        public void LoadPlugins(string path)
        {
            //done:could be multiple plugisn in the same, FILE
            loadedAsm = Assembly.LoadFile(path);
            
                                    
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
