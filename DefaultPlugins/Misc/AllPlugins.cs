using Model.Interface;
using System;
using System.Collections.Generic;
using DefaultPlugins.Misc;
using Model;
using Shared;

namespace DefaultPlugins.Misc
{
    public class AllPlugins
    {
        public static INamedActionPlugin  InvokePlugin(PluginType plugin)
        {
            Type? myType = null;
            switch (plugin)
            {
                case PluginType.FileOpen: 
                    myType = typeof(FileOpenPlugin);
                    break;
                    case PluginType.UrlDecode: 
                    myType = typeof(UrlDecodePlugin);
                    break;
                case PluginType.UrlEncode:
                    myType = typeof(UrlEncodePlugin);
                    break;
                case PluginType.FileSave:
                    myType = typeof(FileSavePlugin);
                    break;
            }
            if (myType == null) throw new NullReferenceException();

            var result= PluginManager.InvokePlugin(myType);
            return result;
        }
        public static List<INamedActionPlugin> Instances { get; } = PluginManager.Instances;
        

    }
}
