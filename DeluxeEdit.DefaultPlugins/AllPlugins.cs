using DeluxeEdit.DefaultPlugins.Managers;
using DeluxeEdit.Model.Interface;
using System;
using System.Collections.Generic;

namespace DeluxeEdit.DefaultPlugins
{
    public enum PluginId { FileOpen,  UrlDecode, UrlEncode}
    public class AllPlugins
    {
        public static INamedActionPlugin  InvokePlugin(PluginId plugin)
        {
            Type? myType = null;
            switch (plugin)
            {
                case PluginId.FileOpen: 
                    myType = typeof(FileOpenPlugin);
                    break;
                    case PluginId.UrlDecode: 
                    myType = typeof(UrlDecodePlugin);
                    break;
                    case PluginId.UrlEncode:
                    myType = typeof(UrlEncodePlugin);
                    break;
            }
            if (myType == null) throw new NullReferenceException();

            var result= PluginManager.InvokePlugin(myType);
            return result;
        }
        public static List<INamedActionPlugin> List()
        {
            var result = new List<INamedActionPlugin>();
            result.Add(PluginManager.InvokePlugin(typeof(FileOpenPlugin)));
            result.Add(PluginManager.InvokePlugin(typeof(UrlEncodePlugin)));
            result.Add(PluginManager.InvokePlugin(typeof(UrlDecodePlugin)));
            return result;
        }

    }
}
