using DeluxeEdit.Model.Interface;
using DeluxeEdit.Model;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.IO;

namespace DeluxeEdit.DefaultPlugins.Managers
{
    //todo:will now instead u se nuget as plugins
    public class PluginManager
    {
        private string myPluginPath;
        public PluginManager()
        {
            myPluginPath = "";
            Assembly? entry =Assembly.GetEntryAssembly();
            //note: path is temporary 
            if (entry!= null)
                myPluginPath = $"{new DirectoryInfo(entry.Location).FullName}\\bin";
 
        }
        private static Assembly? loadedAsm=null;
      
        public void LoadDefaultPlugins()
        { 

        }      
        public void ShowPluginManager()
        {
        }
        public List<INamedActionPlugin> LoadPlugins(string path)
        {
            //done:could be multiple plugisAssemblyn in the same, FILE
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
