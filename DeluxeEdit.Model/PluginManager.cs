using DeluxeEdit.Model.Interface;
using DeluxeEdit.Model;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace DeluxeEdit.Model
{
    //todo:will now instead u se nuget as plugins
    public class PluginManager
    {
        private static Assembly? loadedAsm=null;


        public void ShowPluginManager()
        {
        }
        public List<INamedActionPlugin> LoadPlugins(string path)
        {
            //done:could be multiple plugisn in the same, FILE
            loadedAsm = Assembly.LoadFile(path);
            var result = new List<INamedActionPlugin>();
                                    
            foreach (var t in loadedAsm.GetTypes())
            {
        
                var newItem= Activator.CreateInstance(t);
                var newItemCasted=newItem is INamedActionPlugin ? newItem as INamedActionPlugin : null; ;

                if (newItemCasted != null)
                    result.Add(newItemCasted);
            }
            return result;
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
