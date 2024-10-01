using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeEdit.DefaultPlugins.Util
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Controls;
        public class DeluxeEditUtil
        {
            private List<UserControl> GetUserControls()
            {
                List<UserControl> userControls = new List<UserControl>();
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
                string resourceName = assembly.GetName().Name + ".g.resources";
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                using (var reader = new System.Resources.ResourceReader(stream))
                {
                    foreach (System.Collections.DictionaryEntry entry in reader)
                    {
                        string key = entry.Key.ToString();
                        if (key.StartsWith("view") && key.EndsWith(".baml"))
                        {
                            System.IO.Stream bamlStream = entry.Value as System.IO.Stream;
                            if (bamlStream != null)
                            {
                                using (System.Windows.Baml2006.Baml2006Reader bamlReader = new System.Windows.Baml2006.Baml2006Reader(bamlStream))
                                {
                                    userControls.Add(System.Windows.Markup.XamlReader.Load(bamlReader) as UserControl);
                                }
                            }
                        }
                    }
                }
                return userControls;
            }
        }
    }
}

