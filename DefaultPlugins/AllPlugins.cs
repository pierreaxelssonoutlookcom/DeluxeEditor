using Model.Interface;
using System;
using System.Collections.Generic;
using DefaultPlugins;
using Model;
using Shared;
using System.Linq;
using System.Windows.Controls;

namespace DefaultPlugins
{
    public class AllPlugins
    {
        public static T InvokePlugin<T>(PluginType plugin) where T : class
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
                case PluginType.FileSaveAs:
                    myType = typeof(FileSavePlugin);
                    break;
                case PluginType.FileNew:
                    myType = typeof(FileNewPlugin);
                    break;
                case PluginType.Hex:
                    myType = typeof(HexPlugin);
                    break;

            }
            if (myType == null) throw new NullReferenceException();

            var result = InvokePlugin<T>(myType);
            return result;
        }
        public static T InvokePlugin<T>(string type) where T : class
        {
            T? result = default;
            Type? matchedType = PluginManager.SourceFiles.SelectMany(p => p.MatchingTypes)
             .SingleOrDefault(p => p.ToString() == type);
            if (matchedType != null)
            {
                object? objecctResult = Activator.CreateInstance(matchedType);
                if (objecctResult != null && objecctResult is INamedActionPlugin)
                    result = objecctResult as T;
            }
            return result;
            ;
        }
        public static T InvokePlugin<T>(Type type) where T : class
        {
            var obj = PluginManager.CreateObject(type);
            T? result = default(T);

            if (obj != null && obj is T)
                result = obj as T;

            return result;
        }


        public static T InvokePluginz<T>(PluginItem item) where T : class
        {
            var result = InvokePlugin<T>(item.MyType);
            return result;
        }
        public static IEnumerable<INamedActionPlugin> InvokePlugins(IEnumerable<PluginItem> items)
        {
            var result = items.Select(p => InvokePlugin(p.MyType)).ToList();

            return result;
        }

        public static INamedActionPlugin InvokePlugin(Type type)
        {
            var result = PluginManager.CreateObject(type);
            return result;
        }
        public static INamedActionPlugin InvokePluginz(PluginItem item)
        {
            var result = InvokePlugin(item.MyType);
            return result;
        }
        public static INamedActionPlugin InvokePlugin(string type)
        {
            INamedActionPlugin? result = null;
            Type? matchedType = PluginManager.SourceFiles.SelectMany(p => p.MatchingTypes)
            .SingleOrDefault(p => p.ToString() == type);
            if (matchedType != null)
            {
                object? objecctResult = Activator.CreateInstance(matchedType);
                if (objecctResult != null && objecctResult is INamedActionPlugin)
                    result = objecctResult as INamedActionPlugin;
            }
            return result;
        }
        public static INamedActionPlugin InvokePlugin(PluginType plugin)
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
                case PluginType.FileSaveAs:
                    myType = typeof(FileSavePlugin);
                    break;
                case PluginType.FileNew:
                    myType = typeof(FileNewPlugin);
                    break;
                case PluginType.Hex:
                    myType = typeof(HexPlugin);
                    break;

            }
            if (myType == null) throw new NullReferenceException();

            var result = InvokePlugin(myType);
            return result;
        }

    }
}