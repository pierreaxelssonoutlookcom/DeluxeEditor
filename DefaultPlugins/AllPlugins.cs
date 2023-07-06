using ModelMisc;
using Model.Interface;
using System;
using System.Collections.Generic;

namespace DefaultPlugins
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
        public static List<INamedActionPlugin> Instances { get; } = PluginManager.Instances;
        

    }
}
