using DeluxeEdit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeluxeEdit.Extensions
{
    public class PluginSourceParser
    {
        public PluginSourceParser()
        {
            
        }
        public PluginSourceItem ParseFileName(string path)
        { 
            var result = new PluginSourceItem { LocalPath = path };
            string fileNameFirstPart = path.Split('.')[0];
            var fileNameFirstPartArray = fileNameFirstPart.ToArray();
            for (int i = 0; i<fileNameFirstPartArray.Length - 1; i++)
            {
                if (Char.IsDigit(fileNameFirstPartArray[i]))
                {
                    result.Version = Version.Parse(fileNameFirstPart.Substring(i));
                    result.ID = fileNameFirstPart.Substring(0, i);
                    break;
                }
                if (result.ID == null) result.ID = fileNameFirstPart;
            }
            return result;
       }   
    }
}
