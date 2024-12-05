using System.Reflection;
 
namespace Model
{
    public class PluginFile
    {
        public bool Loaded { get; set; }
        public string Name { get; set; } = String.Empty;
        public Version Version { get; set; } = new Version();

        public string LocalPath { get; set; } = String.Empty;
        public string? Url { get; set; } = null;
        public Assembly? Assembly  { get; set; }
        public List<PluginItem> Plugins { get; set; }  =  new List<PluginItem>();
        public List<Type> MatchingTypes { get; set; } = new List<Type>();


    }
}